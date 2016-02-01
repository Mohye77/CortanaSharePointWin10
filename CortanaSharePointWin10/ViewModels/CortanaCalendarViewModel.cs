using CortanaSharePointWin10.Models;
using CortanaSharePointWin10.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaSharePointWin10.ViewModels
{
    public class CortanaCalendarViewModel : ViewModelBase
    {
        public CortanaCalendarViewModel()
        {
            DataProvider.Instance.LoadCortanaCalendarCompleted += new DataProvider.LoadCortanaCalendarHandler(Instance_LoadAppointmentsCompleted);
        }

        private ObservableCollection<CortanaAppointment> _cortanaAppointments = new ObservableCollection<CortanaAppointment>();
        /// <summary>
        /// Collection des items dans une liste SP
        /// </summary>
        public ObservableCollection<CortanaAppointment> CortanaAppointments
        {
            get
            {
                return _cortanaAppointments;
            }
            private set
            {
                if (value != _cortanaAppointments)
                {
                    _cortanaAppointments = value;
                    NotifyPropertyChanged("CortanaAppointments");
                }
            }
        }

        /// <summary>
        /// Charge les événements du calendrier
        /// </summary>
        public void LoadAppointments(string siteUrl, string calendarName, string login, string mdp)
        {
            DataProvider.Instance.LoadAppointments(siteUrl, calendarName, login, mdp);
        }


        void Instance_LoadAppointmentsCompleted(ObservableCollection<CortanaAppointment> cortanaAppointments)
        {
            if (null == cortanaAppointments && (null == CortanaAppointments || (null != CortanaAppointments && 0 == CortanaAppointments.Count))) { }
            else
            {
                if (null != cortanaAppointments)
                    this.CortanaAppointments = cortanaAppointments;
            }
        }
    }
}
