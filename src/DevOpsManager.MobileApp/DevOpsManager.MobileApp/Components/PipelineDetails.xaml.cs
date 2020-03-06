using DevOpsManager.MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevOpsManager.MobileApp.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PipelineDetails : Grid
    {

        public static readonly BindableProperty ReleaseDefinitionProperty = BindableProperty.Create(nameof(ReleaseDefinition),
                                                                                                    typeof(ReleaseDefinition),
                                                                                                    typeof(PipelineDetails));

        public static readonly BindableProperty RefreshCommandProperty = BindableProperty.Create(nameof(RefreshCommand),
                                                                                                 typeof(Command<ReleaseDefinition>),
                                                                                                 typeof(PipelineDetails));

        public ReleaseDefinition ReleaseDefinition { get => (ReleaseDefinition) GetValue(ReleaseDefinitionProperty); set => SetValue(ReleaseDefinitionProperty, value); }

        public Command<ReleaseDefinition> RefreshCommand { get => (Command<ReleaseDefinition>) GetValue(RefreshCommandProperty); set => SetValue(RefreshCommandProperty, value); }

        public PipelineDetails()
        {
            InitializeComponent();
        }

        private void RefreshBtn_Pressed(object sender, EventArgs e)
        {
            if (RefreshCommand?.CanExecute(ReleaseDefinition) == true)
            {
                RefreshCommand.Execute(ReleaseDefinition);
                releasesCarousel.ItemsSource = ReleaseDefinition.Releases;
            }
        }
    }
}