using CortanaSharePointWin10.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaSharePointWin10.Services
{
    public sealed class DataProvider
    {
        #region Static Instance
        private static DataProvider _instance;
        public static DataProvider Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new DataProvider();
                return _instance;
            }
            private set
            {
                if (value != _instance)
                {
                    _instance = value;
                }
            }
        }
        #endregion

        #region CortanaItems
        public delegate void LoadCortanaItemsHandler(ObservableCollection<CortanaItem> cortanaItems);
        public event LoadCortanaItemsHandler LoadCortanaItemsCompleted;

        /// <summary>
        /// Charge les items de la liste
        /// </summary>
        public void LoadListItems(string siteUrl, string listName, string login, string mdp)
        {
            CortanaItemsRequester cir = new CortanaItemsRequester();

            cir.LoadDataCompleted += new CortanaItemsRequester.LoadDataHandler((cortanaItems) =>
            {
                LoadCortanaItemsCompleted(cortanaItems); // Fire event LoadCortanaItemsCompleted
            }
            );
            cir.LoadItems(siteUrl, listName, login, mdp);
        }
        #endregion

        #region CortanaCalendar
        public delegate void LoadCortanaCalendarHandler(ObservableCollection<CortanaAppointment> cortanaAppointments);
        public event LoadCortanaCalendarHandler LoadCortanaCalendarCompleted;

        /// <summary>
        /// Charge les événements du calendrier
        /// </summary>
        public void LoadAppointments(string siteUrl, string calendarName, string login, string mdp)
        {
            CortanaAppointmentsRequester car = new CortanaAppointmentsRequester();

            car.LoadDataCompleted += new CortanaAppointmentsRequester.LoadDataHandler((cortanaAppointments) =>
            {
                LoadCortanaCalendarCompleted(cortanaAppointments); // Fire event LoadCortanaCalendarCompleted
            }
            );
            car.LoadAppointments(siteUrl, calendarName, login, mdp);
        }
        #endregion

        #region CortanaItems
        public delegate void LoadCortanaSearchHandler(ObservableCollection<CortanaItem> cortanaResults);
        public event LoadCortanaSearchHandler LoadCortanaSearchCompleted;

        /// <summary>
        /// Charge les items de la liste
        /// </summary>
        public void LoadResults(string siteUrl, string keywords, string login, string mdp)
        {
            CortanaSearchRequester csr = new CortanaSearchRequester();

            csr.LoadDataCompleted += new CortanaSearchRequester.LoadDataHandler((cortanaResults) =>
            {
                LoadCortanaSearchCompleted(cortanaResults); // Fire event LoadCortanaItemsCompleted
            }
            );
            csr.LoadResults(siteUrl, keywords, login, mdp);
        }
        #endregion
    }
}
