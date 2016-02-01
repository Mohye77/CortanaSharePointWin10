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
    public class CortanaListItemsViewModel : ViewModelBase
    {

        public CortanaListItemsViewModel()
        {
            DataProvider.Instance.LoadCortanaItemsCompleted += new DataProvider.LoadCortanaItemsHandler(Instance_LoadListItemsCompleted);
        }

        private ObservableCollection<CortanaItem> _cortanaItems = new ObservableCollection<CortanaItem>();
        /// <summary>
        /// Collection des items dans une liste SP
        /// </summary>
        public ObservableCollection<CortanaItem> CortanaItems
        {
            get
            {
                return _cortanaItems;
            }
            private set
            {
                if (value != _cortanaItems)
                {
                    _cortanaItems = value;
                    NotifyPropertyChanged("CortanaItems");
                }
            }
        }

        /// <summary>
        /// Charge les items de la liste
        /// </summary>
        public void LoadListItems(string siteUrl, string listName, string login, string mdp)
        {
            DataProvider.Instance.LoadListItems(siteUrl, listName, login, mdp);
        }


        void Instance_LoadListItemsCompleted(ObservableCollection<CortanaItem> cortanaListItems)
        {
            if (null == cortanaListItems && (null == CortanaItems || (null != CortanaItems && 0 == CortanaItems.Count))) { }
            else
            {
                if (null != cortanaListItems)
                    this.CortanaItems = cortanaListItems;
            }
        }

    }
}
