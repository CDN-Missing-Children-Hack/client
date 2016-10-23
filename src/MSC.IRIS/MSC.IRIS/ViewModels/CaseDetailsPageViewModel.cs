using System;
using MSC.IRIS.Models;
using Xamarin.Forms;

namespace MSC.IRIS.ViewModels
{
    public class CaseDetailsPageViewModel : ViewModelBase
    {
        public CaseDetailsPageViewModel (INavigation navigation) : base (navigation)
        {
            this.Title = "Case Details";
        }

        private Case _Case = null;

        /// <summary>
        /// Sets and gets the Case property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public Case Case
        {
            get
            {
                return _Case;
            }
            set
            {
                _Case = value;
                OnPropertyChanged ();
            }
        }
    }
}
