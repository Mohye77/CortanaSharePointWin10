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
                //using (ClientContext context = new ClientContext(siteUrl))
                //{
                //    SharePointOnlineCredentials credentials = new SharePointOnlineCredentials(login, mdp);
                //    context.Credentials = credentials;
                //    Web web = context.Web;
                //    ListItemCollection calendarAppointmentsCollection = web.Lists.GetByTitle(calendarName).GetItems(CamlQuery.CreateAllItemsQuery());
                //    context.Load(calendarAppointmentsCollection);

                //    await context.ExecuteQueryAsync().ContinueWith((t) =>
                //    {
                //        foreach (var item in calendarAppointmentsCollection)
                //        {
                //            CortanaAppointment ci = new CortanaAppointment();
                //            ci.Id = item.FieldValues["ID"] != null ? item.FieldValues["ID"].ToString() : string.Empty;
                //            ci.Subject = item.FieldValues["Title"] != null ? item.FieldValues["Title"].ToString() : string.Empty;
                //            ci.StartDate = item.FieldValues["EventDate"] != null ? DateTime.Parse(item.FieldValues["EventDate"].ToString()) : new DateTime();
                //            cortanaAppointments.Add(ci);
                //        }
                //    });
                //}

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
