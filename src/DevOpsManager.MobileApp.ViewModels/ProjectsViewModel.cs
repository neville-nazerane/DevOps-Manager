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
        private Command<IEnumerable<Project>> _showingProjectsChangedCommand;
        private IEnumerable<Project> _projects;
        private IEnumerable<Project> _showingProjects;

        public Command ChangeOrgCommand => BuildCommand(ChangeOrgAsync);

        public Command<Project> ToPipelineCommand { get; }

        public Command<Project> UpdateFavoriteCommand { get; }

        public Command<StarredContext> StarCommand { get; }

        public bool IsFavoritesShowing
        {
            get => _isFavoritesShowing;
            set
            {
                _isFavoritesShowing = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(ShowingProjects));
            }
        }

        public IEnumerable<Project> Projects { get => _projects; set => SetProperty(ref _projects, value); }

        public IEnumerable<Project> ShowingProjects { get => _showingProjects; set => SetProperty(ref _showingProjects, value); }
        public Command<IEnumerable<Project>> ShowingProjectsChangedCommand 
                        { get => _showingProjectsChangedCommand; 
            set => SetProperty(ref _showingProjectsChangedCommand, value); }

        public ProjectsViewModel(DevOpsService devOpsService, PersistantState persistantState, FavoriteService favoriteService)
        {
            _devOpsService = devOpsService;
            _persistantState = persistantState;
            _favoriteService = favoriteService;

            ToPipelineCommand = BuildCommand<Project>(GoToPipelinesAsync);
            //UpdateFavoriteCommand = new Command<Project>(_favoriteService.UpdateProject);
            ShowingProjectsChangedCommand = new Command<IEnumerable<Project>>(p => ShowingProjects = p);
            StarCommand = new Command<StarredContext>(StarChanged);
        }

        private Task ChangeOrgAsync()
        {
            _persistantState.Organization = null;
            return NavigateAsync<AccountsViewModel>();
        }

        private void StarChanged(StarredContext context)
        {
            if (context.IsStarred)
                _favoriteService.AddProject(context.Identifier);
            else
                _favoriteService.RemoveProject(context.Identifier);
        }

        public override async Task InitAsync()
        {
            if (!string.IsNullOrEmpty(_persistantState.Project))
                await GoToPipelinesAsync(_persistantState.Project);
            else
            {
                var projects = await _devOpsService.GetProjectsAsync();
                _favoriteService.UpdateToProjects(projects.Value);
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
