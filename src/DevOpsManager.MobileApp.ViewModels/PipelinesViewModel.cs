using DevOpsManager.MobileApp.Models;
using DevOpsManager.MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DevOpsManager.MobileApp.ViewModels
{
    public class PipelinesViewModel : ViewModelBase
    {
        private readonly DevOpsService _devOpsService;
        private readonly PersistantState _persistantState;
        private ObservableCollection<ReleaseDefinition> _releaseDefinitions;

        public Command ToProjectsCommand => BuildCommand(ToProjectAsync);
        public Command<ReleaseDefinition> LoadReleaseCommand => BuildCommand<ReleaseDefinition>(LoadReleasesAsync);

        public ObservableCollection<ReleaseDefinition> ReleaseDefinitions
        {
            get => _releaseDefinitions; 
            set
            {
                _releaseDefinitions = value;
                OnPropertyChanged();
            }
        }

        public PipelinesViewModel(DevOpsService devOpsService, PersistantState persistantState)
        {
            _devOpsService = devOpsService;
            _persistantState = persistantState;
        }

        public override async Task InitAsync()
        {
            ReleaseDefinitions = await _devOpsService.GetReleaseDefinitionsAsync();
        }

        private Task ToProjectAsync()
        {
            _persistantState.Project = null;
            return NavigateAsync<ProjectsViewModel>();
        }

        private async Task LoadReleasesAsync(ReleaseDefinition definition)
        {
            definition.Releases = await _devOpsService.GetReleasesAsync(definition.Id.ToString());
        }

    }
}
