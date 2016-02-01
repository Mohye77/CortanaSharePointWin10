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
                //using (ClientContext context = new ClientContext(siteUrl))
                //{
                //    SharePointOnlineCredentials credentials = new SharePointOnlineCredentials(login, mdp);
                //    context.Credentials = credentials;

                //    KeywordQuery keywordQuery = new KeywordQuery(context);
                //    keywordQuery.QueryText = keywords;
                //    keywordQuery.SourceId = new Guid(SettingsValues.SourceId);
                //    keywordQuery.EnablePhonetic = true;
                //    keywordQuery.EnableStemming = true;
                //    keywordQuery.RowLimit = 100;

                //    SearchExecutor searchExecutor = new SearchExecutor(context);

                //    ClientResult<ResultTableCollection> results = searchExecutor.ExecuteQuery(keywordQuery);

                //    context.ExecuteQueryAsync().Wait();

                //    foreach (var item in results.Value[0].ResultRows)
                //    {
                //        CortanaItem ci = new CortanaItem();
                //        ci.Id = item["ID"] != null ? item["ID"].ToString() : string.Empty;
                //        ci.Title = item["Title"] != null ? item["Title"].ToString() : string.Empty;
                //        ci.LastModifiedDate = item["Modified"] != null ? item["Modified"].ToString() : string.Empty;
                //        ci.Url = item["Path"] != null ? item["Path"].ToString() : string.Empty;
                //        cortanaResults.Add(ci);
                //    }
                //}

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
