using System;
using System.Threading.Tasks;
using System.Windows.Input;
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
                    System.Diagnostics.Debug.WriteLine ($"Running {nameof (LoginButtonTappedCommand)}");
                    this.IsBusy = true;
                    this.ErrorText = string.Empty;
                    try
                    {
                        var user = await Api.Login (this.Username, this.Password);
                        await Task.Delay (2500);
                        if (user == null)
                        {
                            // there was some issue
                            System.Diagnostics.Debug.WriteLine ($"Unable to authenticate user {this.Username}");
                            this.ErrorText = $"Unable to authenticate user {this.Username}";
                        }
                        else
                        {
                            // we are good!
                            System.Diagnostics.Debug.WriteLine ("Authenticated {this.Username}");
                            await this.Navigation.PushAsync (new CasesPage ());

                            // clear out the username/password
                            this.Username = string.Empty;
                            this.Password = string.Empty;
                        }
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }));
            }
        }

    }
}
