using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MSC.IRIS.Models;
using Xamarin.Forms;

namespace MSC.IRIS.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged 
    {

        public ViewModelBase (INavigation navigation)
        {
            this.Navigation = navigation;
        }

        /// <summary>
        /// Allows navigation to be handled from the viewmodels
        /// </summary>
        /// <value>The navigation.</value>
        internal INavigation Navigation { get; private set;}

        /// <summary>
        /// Gets the API service that is used in the system
        /// </summary>
        /// <value>The API.</value>
        internal ApiServices Api { get; } = new ApiServices ();

        private bool _IsBusy = false;

        /// <summary>
        /// Sets and gets the IsBusy property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                OnPropertyChanged ();
            }
        }

        private string _Title = null;

        /// <summary>
        /// Sets and gets the Title property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                OnPropertyChanged ();
            }
        }

        /// <summary>
        /// Occurs when property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed event if required
        /// </summary>
        /// <param name="name">Name.</param>
        internal void OnPropertyChanged ([CallerMemberName]string name = "") =>
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (name));

    }
}
