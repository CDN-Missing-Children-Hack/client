using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;

namespace MSC.IRIS.ViewModels
{
    /// <summary>
    /// Login page view model.
    /// </summary>
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel (INavigation navigation) : base(navigation)
        {
            this.Title = "Login";
        }

        private string _Username = null;

        /// <summary>
        /// Sets and gets the Username property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                    OnPropertyChanged ();
                }
            }
        }

        private string _Password = null;

        /// <summary>
        /// Sets and gets the Password property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    OnPropertyChanged ();
                }
            }
        }

        private string _ErrorText = null;

        /// <summary>
        /// Sets and gets the ErrorText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ErrorText
        {
            get
            {
                return _ErrorText;
            }
            set
            {
                _ErrorText = value;
                OnPropertyChanged ();
            }
        }

        private ICommand _LoginButtonTappedCommand;
        /// <summary>
        /// Gets the LoginButtonTappedCommand.
        /// </summary>
        public ICommand LoginButtonTappedCommand
        {
            get
            {
                return _LoginButtonTappedCommand
                    ?? (_LoginButtonTappedCommand = new Command (async () =>
                {
                    Log($"Running {nameof (LoginButtonTappedCommand)}");

                    // check to see user entered something
                    if (string.IsNullOrEmpty (this.Username))
                    {
                        this.ErrorText = "Please type in a username";
                        return;
                    }
                    if (string.IsNullOrEmpty (this.Password))
                    {
                        this.ErrorText = "Please type in a password";
                        return;
                    }

                    this.IsBusy = true;
                    this.ErrorText = string.Empty;
                    try
                    {
                        var user = await Api.Login (this.Username, this.Password);
                        if (user == null)
                        {
                            // there was some issue
                            Log ($"Unable to authenticate user {this.Username}");
                            this.ErrorText = $"Unable to authenticate user {this.Username}";
                        }
                        else
                        {
                            // we are good!
                            Log ("Authenticated {this.Username}");

                            // setup the next VM
                            var vm = SimpleIoc.Default.GetInstance<CasesPageViewModel> ();
                            vm.User = user;
                            vm.GetCasesCommand.Execute (null);

                            // navigate to the cases page
                            await this.Navigation.PushAsync (new CasesPage ());

                            // clear out the username/password
                            this.Username = string.Empty;
                            this.Password = string.Empty;
                        }
                    }
                    finally
                    {
                        IsBusy = false;
                        Log ($"Done running {nameof (LoginButtonTappedCommand)}");
                    }
                }));
            }
        }

    }
}
