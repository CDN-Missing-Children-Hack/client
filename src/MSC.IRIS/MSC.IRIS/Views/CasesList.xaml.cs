using MSC.IRIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MSC.IRIS.Views
{
    public partial class CasesList : ContentPage
    {
        // Trying to find out why "CaseType" enum not being set from CasesPage...
        public static readonly BindableProperty CaseTypeProperty = BindableProperty.CreateAttached(
            "CaseType",
            typeof(ViewModels.CasesPageViewModel.CASE_TYPE),
            typeof(CasesList),
            ViewModels.CasesPageViewModel.CASE_TYPE.OPEN,
            validateValue: (bindable, value) => Enum.IsDefined(typeof(ViewModels.CasesPageViewModel.CASE_TYPE), value),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var v = (ViewModels.CasesPageViewModel.CASE_TYPE)newValue;
                ((CasesList)bindable).CaseType =v;
                (bindable.BindingContext as CasesPageViewModel).CaseType = v;
                switch(v)
                {
                    case CasesPageViewModel.CASE_TYPE.ARCHIVED:
                        ((CasesList)bindable).casesList.ItemsSource = (bindable.BindingContext as CasesPageViewModel).Archived;
                        break;
                    case CasesPageViewModel.CASE_TYPE.OPEN:
                        ((CasesList)bindable).casesList.ItemsSource = (bindable.BindingContext as CasesPageViewModel).Open;
                        break;
                }
            }

        );
        public CasesList()
        {
            // hide the backbutton
            NavigationPage.SetHasBackButton(this, false);

            InitializeComponent();

            // set the binding context
            this.BindingContext = ViewModelLocator.Default.GetViewModel<CasesPageViewModel>();

            var vm = ViewModelLocator.Default.GetViewModel<CasesPageViewModel>();
            vm.PropertyChanged += (e, s) =>
            {
                if (s.PropertyName == nameof(CasesPageViewModel.Cases))
                {
                    switch (this.CaseType)
                    {
                        case CasesPageViewModel.CASE_TYPE.ARCHIVED:
                            casesList.ItemsSource = vm.Archived;
                            break;
                        case CasesPageViewModel.CASE_TYPE.OPEN:
                            casesList.ItemsSource = vm.Open;
                            break;
                    }
                }
            };

            // wire up teh tapped event
            casesList.ItemTapped += (sender, args) =>
            {
                (this.BindingContext as CasesPageViewModel).CaseSelectedCommand.Execute(casesList.SelectedItem);
                casesList.SelectedItem = null;
            };
        }

        public ViewModels.CasesPageViewModel.CASE_TYPE CaseType { get; set; }

    }
}
