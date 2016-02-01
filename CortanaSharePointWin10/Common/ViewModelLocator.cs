using CortanaSharePointWin10.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CortanaSharePointWin10.Common
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
        }

        #region CortanaListItems
        private static CortanaListItemsViewModel _cortanaListItemsInstance;
        public static CortanaListItemsViewModel CortanaListItemsStatic
        {
            get
            {
                if (_cortanaListItemsInstance == null)
                    CreateCortanaListItemsInstance();
                return _cortanaListItemsInstance;
            }
        }

        public static void CreateCortanaListItemsInstance()
        {
            if (_cortanaListItemsInstance == null)
                _cortanaListItemsInstance = new CortanaListItemsViewModel();
        }

        public CortanaListItemsViewModel CortanaListItemsInstance
        {
            get { return CortanaListItemsStatic; }
        }

        public static void ClearCortanaListItemsInstance()
        {
            _cortanaListItemsInstance = null;
        }
        #endregion

        #region CortanaCalendar
        private static CortanaCalendarViewModel _cortanaCalendarInstance;
        public static CortanaCalendarViewModel CortanaCalendarStatic
        {
            get
            {
                if (_cortanaCalendarInstance == null)
                    CreateCortanaCalendarInstance();
                return _cortanaCalendarInstance;
            }
        }

        public static void CreateCortanaCalendarInstance()
        {
            if (_cortanaCalendarInstance == null)
                _cortanaCalendarInstance = new CortanaCalendarViewModel();
        }

        public CortanaCalendarViewModel CortanaCalendarInstance
        {
            get { return CortanaCalendarStatic; }
        }

        public static void ClearCortanaCalendarInstance()
        {
            _cortanaCalendarInstance = null;
        }
        #endregion

        #region CortanaSearch
        private static CortanaSearchViewModel _cortanaSearchInstance;
        public static CortanaSearchViewModel CortanaSearchStatic
        {
            get
            {
                if (_cortanaSearchInstance == null)
                    CreateCortanaSearchInstance();
                return _cortanaSearchInstance;
            }
        }

        public static void CreateCortanaSearchInstance()
        {
            if (_cortanaSearchInstance == null)
                _cortanaSearchInstance = new CortanaSearchViewModel();
        }

        public CortanaSearchViewModel CortanaSearchInstance
        {
            get { return CortanaSearchStatic; }
        }

        public static void ClearCortanaSearchInstance()
        {
            _cortanaSearchInstance = null;
        }
        #endregion
    }
}
