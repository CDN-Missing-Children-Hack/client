using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MSC.IRIS.Models;
using Xamarin.Forms;

namespace MSC.IRIS.ViewModels
{
    public class CasesPageViewModel : ViewModelBase
    {
        public CasesPageViewModel (INavigation navigation) : base (navigation)
        {
        }

        private PoliceUser _User = null;

        /// <summary>
        /// Sets and gets the User property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public PoliceUser User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                OnPropertyChanged ();
            }
        }

        private ObservableCollection<Case> _Cases = null;

        /// <summary>
        /// Sets and gets the Cases property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<Case> Cases
        {
            get
            {
                return _Cases;
            }
            set
            {
                _Cases = value;
                OnPropertyChanged ();
            }
        }

        private ICommand _GetCasesCommand;
        /// <summary>
        /// Gets the GetCases.
        /// </summary>
        public ICommand GetCasesCommand
        {
            get
            {
                return _GetCasesCommand
                    ?? (_GetCasesCommand = new Command (
                                          async () =>
                                          {
                                              Log ($"Running {nameof (GetCasesCommand)}");

                                              try
                                              {
                                                  // get the API
                                                  var resp = await this.Api.GetCases(this.User);

                                                  // set the collection property to update the API
                                                  this.Cases = new ObservableCollection<Case>(resp);
                                              }
                                              finally
                                              {
                                                  Log ($"Done running {nameof (GetCasesCommand)}");
                                              }
                                          }));
            }
        }
    }
}
