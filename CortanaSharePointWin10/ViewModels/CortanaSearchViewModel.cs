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
    public class CortanaSearchViewModel : ViewModelBase
    {
        public CortanaSearchViewModel()
        {
            DataProvider.Instance.LoadCortanaSearchCompleted += new DataProvider.LoadCortanaSearchHandler(Instance_LoadResultsCompleted);
        }

        private ObservableCollection<CortanaItem> _cortanaResults = new ObservableCollection<CortanaItem>();
        /// <summary>
        /// Collection des résultats de recherche
        /// </summary>
        public ObservableCollection<CortanaItem> CortanaResults
        {
            get
            {
                return _cortanaResults;
            }
            private set
            {
                if (value != _cortanaResults)
                {
                    _cortanaResults = value;
                    NotifyPropertyChanged("CortanaResults");
                }
            }
        }

        /// <summary>
        /// Charge les résultats de recherche
        /// </summary>
        public void LoadResults(string siteUrl, string keywords, string login, string mdp)
        {
            DataProvider.Instance.LoadResults(siteUrl, keywords, login, mdp);
        }


        void Instance_LoadResultsCompleted(ObservableCollection<CortanaItem> cortanaResults)
        {
            if (null == cortanaResults && (null == CortanaResults || (null != CortanaResults && 0 == CortanaResults.Count))) { }
            else
            {
                if (null != cortanaResults)
                    this.CortanaResults = cortanaResults;
            }
        }
    }
}
