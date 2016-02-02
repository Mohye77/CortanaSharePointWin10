using CortanaSharePointWin10.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaSharePointWin10.Services
{
    public class CortanaAppointmentsRequester
    {
        public CortanaAppointmentsRequester()
        { }

        public async void LoadAppointments(string siteUrl, string calendarName, string login, string mdp)
        {
            ObservableCollection<CortanaAppointment> cortanaAppointments = new ObservableCollection<CortanaAppointment>();

            try
            {

                CortanaAppointment ci = new CortanaAppointment();
                ci.Id = "1";
                ci.Subject = "Titre de l'event";
                ci.StartDate = DateTime.Now.AddDays(-1).AddHours(10);
                cortanaAppointments.Add(ci);
                LoadDataCompleted(cortanaAppointments); // Fire event DataLoadCompleted

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erreur : " + ex.Message);
                LoadDataCompleted(null);
            }
        }

        public delegate void LoadDataHandler(ObservableCollection<CortanaAppointment> cortanaAppointments);
        public event LoadDataHandler LoadDataCompleted;
    }
}
