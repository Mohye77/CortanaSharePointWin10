using CortanaSharePointWin10.Common;
using CortanaSharePointWin10.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class CortanaCalendar : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public CortanaCalendar()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;

            DataContext = ViewModelLocator.CortanaCalendarStatic;

            Loaded += CortanaCalendar_Loaded;
        }

        private void CortanaCalendar_Loaded(object sender, RoutedEventArgs e)
        {
            CortanaCalendarViewModel viewModel = DataContext as CortanaCalendarViewModel;
            viewModel.LoadAppointments(SettingsValues.SiteUrl, SettingsValues.CalendarTitle, SettingsValues.LoginName, SettingsValues.Password);
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
        #region AppBar events
        private void AppBarBtnHome_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage), null);
        }
        #endregion

        private void CalendarView_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            IList<DateTimeOffset> selectedDates = (sender as CalendarView).SelectedDates;
            if (selectedDates != null && selectedDates.Count > 0)
            {
                appointmentsList.ItemsSource = (DataContext as CortanaCalendarViewModel).CortanaAppointments.Where(app => app.StartDate.Date == selectedDates[0].Date);
            }
        }

        private void Calendar_CalendarViewDayItemChanging(CalendarView sender, CalendarViewDayItemChangingEventArgs args)
        {
            // Render basic day items.
            if (args.Phase == 0)
            {
                // Register callback for next phase.
                args.RegisterUpdateCallback(Calendar_CalendarViewDayItemChanging);
            }
            // Set blackout dates.
            else if (args.Phase == 1)
            {
                // Register callback for next phase.
                args.RegisterUpdateCallback(Calendar_CalendarViewDayItemChanging);
            }
            // Set density bars.
            else if (args.Phase == 2)
            {
                // Avoid unnecessary processing.
                // You don't need to set bars on past dates
                if (args.Item.Date >= DateTimeOffset.Now)
                {
                    var spEvents = (DataContext as CortanaCalendarViewModel).CortanaAppointments.Where(app => app.StartDate.Date == args.Item.Date.Date);

                    List<Color> densityColors = new List<Color>();
                    // Set a density bar color for each of the days bookings.
                    // It's assumed that there can't be more than 10 bookings in a day. Otherwise,
                    // further processing is needed to fit within the max of 10 density bars.
                    int cpt = 0;
                    foreach (var spEvent in spEvents.TakeWhile(a => cpt < 10))
                    {
                        cpt++;
                        densityColors.Add(Colors.Cyan);
                    }
                    args.Item.SetDensityColors(densityColors);
                }
            }
        }
    }
}
