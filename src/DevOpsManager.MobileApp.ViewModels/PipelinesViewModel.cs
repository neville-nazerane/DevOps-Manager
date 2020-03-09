using DevOpsManager.MobileApp.Models;
using DevOpsManager.MobileApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

        public Command SwapFavoriteCommand => new Command(() => ShowFavorites = !ShowFavorites);

        public bool ShowFavorites
        {
            get => Preferences.Get("pipelinesfav", false); 
            set
            {
                Preferences.Set("pipelinesfav", value);
                UpdateDisplay();
                OnPropertyChanged();
            }
        }

        public Command<StarredContext> StarCommand { get; set; }

        public ObservableCollection<ReleaseDefinition> ReleaseDefinitions { get => _releaseDefinitions; set => SetProperty(ref _releaseDefinitions, value); }

        private IEnumerable<ReleaseDefinition> storedDefinitions;

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
            var res = await _devOpsService.GetReleaseDefinitionsAsync();
            storedDefinitions = res.Value;
            favorites = _favoriteService.GetReleaseDef(_persistantState.Project);
            foreach (var def in storedDefinitions)
            {
                def.IsFavorite = favorites.Contains(def.Id.ToString());
            }
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (ShowFavorites)
                ReleaseDefinitions = new ObservableCollection<ReleaseDefinition>(storedDefinitions.Where(d => d.IsFavorite));
            else
                ReleaseDefinitions = new ObservableCollection<ReleaseDefinition>(storedDefinitions);
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
            {
                favorites.Remove(context.Identifier);
                if (ShowFavorites) UpdateDisplay();
            }

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
