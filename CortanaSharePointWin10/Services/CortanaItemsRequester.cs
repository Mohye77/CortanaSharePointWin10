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
                CortanaItem ci = new CortanaItem();
                ci.Id = "1";
                ci.Title = "Titre élément";
                ci.LastModifiedDate = DateTime.Now.ToString();
                ci.Url = "http://www.google.fr";
                cortanaItems.Add(ci);
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
