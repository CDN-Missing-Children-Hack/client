using System;
using System.Windows.Input;
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

        private ICommand _SocialContentItemTappedCommand;
        /// <summary>
        /// Gets the SocialContentItemTapped.
        /// </summary>
        public ICommand SocialContentItemTappedCommand
        {
            get
            {
                return _SocialContentItemTappedCommand
                    ?? (_SocialContentItemTappedCommand = new Command<SocialContent> (
                                          async (item) =>
                                          {
                    if (item != null && item.IsTwitter)
                                              {
                                                  Log ($"Running {nameof (SocialContentItemTappedCommand)}");
                                                  Device.OpenUri (new Uri ($"https://twitter.com/markarteaga/status/{item.Id}"));
                                              }
                                          }));
            }
        }
    }
}
