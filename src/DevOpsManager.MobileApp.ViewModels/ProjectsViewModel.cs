using DevOpsManager.MobileApp.Models;
using DevOpsManager.MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DevOpsManager.MobileApp.ViewModels
{
    public class ProjectsViewModel : ViewModelBase
    {
        private readonly DevOpsService _devOpsService;
        private readonly PersistantState _persistantState;
        private ObservableCollection<Project> _projects;

        public Command ChangeOrgCommand => BuildCommand(NavigateAsync<AccountsViewModel>);

        public Command<Project> ToPipelineCommand => BuildCommand<Project>(GoToPipelinesAsync);

        public ObservableCollection<Project> Projects
        {
            get => _projects; 
            set
            {
                _projects = value;
                OnPropertyChanged();
            }
        }

        public ProjectsViewModel(DevOpsService devOpsService, PersistantState persistantState)
        {
            _devOpsService = devOpsService;
            _persistantState = persistantState;
        }

        public override async Task InitAsync()
        {
            Projects = await _devOpsService.GetProjectsAsync();
        }

        private async Task GoToPipelinesAsync(Project project)
        {
            _persistantState.Project = project.Id;
            await NavigateAsync<PipelinesViewModel>();
        }

    }
}
