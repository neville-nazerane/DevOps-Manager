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
        private readonly FavoriteService _favoriteService;
        private ObservableCollection<ReleaseDefinition> _releaseDefinitions;

        public Command ToProjectsCommand => BuildCommand(ToProjectAsync);
        
        public Command<ReleaseDefinition> LoadReleaseCommand { get; private set; }
        
        public Command<StarredContext> StarCommand { get; set; }

        public ObservableCollection<ReleaseDefinition> ReleaseDefinitions
        {
            get => _releaseDefinitions; 
            set
            {
                _releaseDefinitions = value;
                OnPropertyChanged();
            }
        }

        private ICollection<string> favorites;

        public PipelinesViewModel(DevOpsService devOpsService, PersistantState persistantState, FavoriteService favoriteService)
        {
            _devOpsService = devOpsService;
            _persistantState = persistantState;
            _favoriteService = favoriteService;

            StarCommand = new Command<StarredContext>(SetFavorite);
            LoadReleaseCommand = BuildCommand<ReleaseDefinition>(LoadReleasesAsync);
        }

        public override async Task InitAsync()
        {
            ReleaseDefinitions = await _devOpsService.GetReleaseDefinitionsAsync();
            favorites = _favoriteService.GetReleaseDef(_persistantState.Project);
            foreach (var def in ReleaseDefinitions)
            {
                def.IsFavorite = favorites.Contains(def.Id.ToString());
            }
        }

        private Task ToProjectAsync()
        {
            _persistantState.Project = null;
            return NavigateAsync<ProjectsViewModel>();
        }

        private void SetFavorite(StarredContext context)
        {
            if (context.IsStarred)
                favorites.Add(context.Identifier);
            else
                favorites.Remove(context.Identifier);
            _favoriteService.UpdateReleaseDef(_persistantState.Project, favorites);
        }

        private async Task LoadReleasesAsync(ReleaseDefinition definition)
        {
            definition.Releases = await _devOpsService.GetReleasesAsync(definition.Id.ToString());
        }

        //private async Task GetDetailsAsync(ReleaseContext context)
        //{
        //    var releases = await _devOpsService.GetReleasesAsync(context.ReleaseId);
        //    context.Releases = releases.Value;
        //}

    }
}
