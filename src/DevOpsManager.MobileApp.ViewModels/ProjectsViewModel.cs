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
        private readonly FavoriteService _favoriteService;
        private bool _isFavoritesShowing;
        private Command<IEnumerable<IFavourable>> _showingProjectsChangedCommand;
        private IEnumerable<Project> _projects;
        private IEnumerable<Project> _showingProjects;
        private ICollection<string> favorites;

        public Command ChangeOrgCommand => BuildCommand(ChangeOrgAsync);

        public Command<Project> ToPipelineCommand { get; }

        public Command<Project> UpdateFavoriteCommand { get; }

        public Command<StarredContext> StarCommand { get; }

        public bool IsFavoritesShowing
        {
            get => _isFavoritesShowing;
            set
            {
                SetProperty(ref _isFavoritesShowing, value);
                UpdateShowingProjects();
            }
        }

        public IEnumerable<Project> Projects
        {
            get => _projects; 
            set
            {
                _projects = value;
                UpdateShowingProjects();
            }
        }

        public IEnumerable<Project> ShowingProjects { get => _showingProjects; 
            set => SetProperty(ref _showingProjects, value); }

        public ProjectsViewModel(DevOpsService devOpsService, PersistantState persistantState, FavoriteService favoriteService)
        {
            _devOpsService = devOpsService;
            _persistantState = persistantState;
            _favoriteService = favoriteService;

            ToPipelineCommand = BuildCommand<Project>(GoToPipelinesAsync);
            StarCommand = new Command<StarredContext>(StarChanged);
        }

        private Task ChangeOrgAsync()
        {
            _persistantState.Organization = null;
            return NavigateAsync<AccountsViewModel>();
        }

        private void UpdateShowingProjects()
        {
            if (IsFavoritesShowing)
                ShowingProjects = Projects.Where(p => p.IsFavorite);
            else
                ShowingProjects = Projects;
        }

        private void StarChanged(StarredContext context)
        {
            if (context.IsStarred)
                favorites.Add(context.Identifier);
            else
                favorites.Remove(context.Identifier);
            _favoriteService.UpdateProjects(_persistantState.Organization, favorites);
            UpdateShowingProjects();
        }

        public override async Task InitAsync()
        {
            if (!string.IsNullOrEmpty(_persistantState.Project))
                await GoToPipelinesAsync(_persistantState.Project);
            else
            {
                var projects = await _devOpsService.GetProjectsAsync();

                favorites = _favoriteService.GetProjects(_persistantState.Organization);

                foreach (var project in projects.Value)
                {
                    if (favorites.Contains(project.Id))
                        project.IsFavorite = true;
                }

                Projects = projects.Value;
            }
        }

        private Task GoToPipelinesAsync(Project project) => GoToPipelinesAsync(project.Id);

        private Task GoToPipelinesAsync(string projectId)
        {
            _persistantState.Project = projectId;
            return NavigateAsync<PipelinesViewModel>();
        }

    }
}
