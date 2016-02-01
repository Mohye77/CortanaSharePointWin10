using CortanaSharePointWin10.Common;
using CortanaSharePointWin10.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, voir la page http://go.microsoft.com/fwlink/?LinkId=234238

namespace CortanaSharePointWin10.Views
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class CortanaSearch : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public CortanaSearch()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            DataContext = ViewModelLocator.CortanaSearchStatic;
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        #region navigation
        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.txtCortanaMessages.Text = "Je vous écoute...";
            Windows.Globalization.Language langFR = new Windows.Globalization.Language("fr-FR");
            SpeechRecognizer recognizer = new SpeechRecognizer(langFR);

            SpeechRecognitionTopicConstraint topicConstraint
                    = new SpeechRecognitionTopicConstraint(SpeechRecognitionScenario.Dictation, "Development");

            recognizer.Constraints.Add(topicConstraint);
            await recognizer.CompileConstraintsAsync(); // Required

            var recognition = recognizer.RecognizeAsync();
            recognition.Completed += this.Recognition_Completed;
        }

        /// <summary>
        /// Speech recognition completed.
        /// </summary>
        private async void Recognition_Completed(IAsyncOperation<SpeechRecognitionResult> asyncInfo, AsyncStatus asyncStatus)
        {
            var results = asyncInfo.GetResults();

            if (results.Confidence != SpeechRecognitionConfidence.Rejected)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, new DispatchedHandler(
                    () =>
                    {
                        this.txtCortanaMessages.Text = "Je recherche : " + results.Text + "...";

                        CortanaSearchViewModel viewModel = DataContext as CortanaSearchViewModel;
                        viewModel.PropertyChanged += viewModel_PropertyChanged;
                        viewModel.LoadResults(SettingsValues.SiteUrl, results.Text, SettingsValues.LoginName, SettingsValues.Password);

                    }));
            }
            else
            {
                this.txtCortanaMessages.Text = "Désolé, je n'ai pas compris.";
            }
        }

        void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            int found = (DataContext as CortanaSearchViewModel).CortanaResults.Count;
            if (found == 0)
            {
                this.txtCortanaMessages.Text = "Désolé, je n'ai rien trouvé.";
            }
            else
            {
                this.txtCortanaMessages.Text = "J'ai trouvé " + found + " éléments.";
            }
        }
    }
}
