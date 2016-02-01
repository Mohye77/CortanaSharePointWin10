using CortanaSharePointWin10.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaSharePointWin10.Services
{
    public class CortanaItemsRequester
    {
        public CortanaItemsRequester()
        { }

        public async void LoadItems(string siteUrl, string listName, string login, string mdp)
        {
            ObservableCollection<CortanaItem> cortanaItems = new ObservableCollection<CortanaItem>();

            try
            {
                //using (ClientContext context = new ClientContext(siteUrl))
                //{
                //    SharePointOnlineCredentials credentials = new SharePointOnlineCredentials(login, mdp);
                //    context.Credentials = credentials;
                //    Web web = context.Web;
                //    ListItemCollection listItemCollection = web.Lists.GetByTitle(listName).GetItems(CamlQuery.CreateAllItemsQuery());
                //    context.Load(listItemCollection);

                //    await context.ExecuteQueryAsync().ContinueWith((t) =>
                //    {
                //        foreach (var item in listItemCollection)
                //        {
                //            CortanaItem ci = new CortanaItem();
                //            ci.Id = item.FieldValues["ID"] != null ? item.FieldValues["ID"].ToString() : string.Empty;
                //            ci.Title = item.FieldValues["Title"] != null ? item.FieldValues["Title"].ToString() : string.Empty;
                //            ci.LastModifiedDate = item.FieldValues["Modified"] != null ? item.FieldValues["Modified"].ToString() : string.Empty;
                //            ci.Url = item.FieldValues["FileRef"] != null ? item.FieldValues["FileRef"].ToString() : string.Empty;
                //            cortanaItems.Add(ci);
                //        }
                //    });
                //}

                LoadDataCompleted(cortanaItems); // Fire event DataLoadCompleted

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erreur : " + ex.Message);
                LoadDataCompleted(null);
            }
        }

        public delegate void LoadDataHandler(ObservableCollection<CortanaItem> cortanaItems);
        public event LoadDataHandler LoadDataCompleted;
    }
}
