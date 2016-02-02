using CortanaSharePointWin10.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaSharePointWin10.Services
{
    public class CortanaSearchRequester
    {
        public CortanaSearchRequester()
        { }

        public async void LoadResults(string siteUrl, string keywords, string login, string mdp)
        {
            ObservableCollection<CortanaItem> cortanaResults = new ObservableCollection<CortanaItem>();

            keywords = keywords.Trim('.').ToLower();

            try
            {

                CortanaItem ci = new CortanaItem();
                ci.Id = "1";
                ci.Title = "Titre élément";
                ci.LastModifiedDate = DateTime.Now.ToString();
                ci.Url = "http://www.cellenza.com";
                cortanaResults.Add(ci);

                LoadDataCompleted(cortanaResults); // Fire event DataLoadCompleted

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Erreur : " + ex.Message);
                LoadDataCompleted(null);
            }
        }

        public delegate void LoadDataHandler(ObservableCollection<CortanaItem> cortanaResults);
        public event LoadDataHandler LoadDataCompleted;
    }
}
