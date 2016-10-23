using System;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MSC.IRIS.ViewModels;

namespace MSC.IRIS
{
    public class ViewModelLocator
    {
        private static ViewModelLocator _default;
        public static ViewModelLocator Default
        {
            get
            {
                return _default ?? (_default = new ViewModelLocator ());
            }
        }

        private ViewModelLocator ()
        {
            
            // Register all the models
            SimpleIoc.Default.Register<LoginPageViewModel> ();
            SimpleIoc.Default.Register<CaseDetailsPageViewModel> ();
            SimpleIoc.Default.Register<CasesPageViewModel> ();
            SimpleIoc.Default.Register<MapPageViewModel> ();
        }

        public T GetViewModel<T> ()
        {
            return ServiceLocator.Current.GetInstance<T> ();
        }

    }
}
