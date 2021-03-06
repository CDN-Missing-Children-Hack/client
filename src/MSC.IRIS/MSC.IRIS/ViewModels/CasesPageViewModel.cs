﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Ioc;
using MSC.IRIS.Models;
using Xamarin.Forms;
using System.Linq;

namespace MSC.IRIS.ViewModels
{
    public class CasesPageViewModel : ViewModelBase
    {
        public enum CASE_TYPE { OPEN, ARCHIVED };

        public CASE_TYPE CaseType { get; set; }

        public CasesPageViewModel(INavigation navigation) : base(navigation)
        {
            CaseType = CASE_TYPE.OPEN;
            this.Title = "Cases";
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

        private bool _IsLoading = false;

        /// <summary>
        /// Sets and gets the IsLoading property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return _IsLoading;
            }
            set
            {
                _IsLoading = value;
                OnPropertyChanged ();
            }
        }

        public List<Models.Case> Open { get; set; }
        public List<Models.Case> Archived { get; set; }

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
                                              this.IsBusy = true;
                                              if (Cases == null || Cases.Count == 0)
                                                this.IsLoading = true;
                                              try
                                              {
                                                  // get the API
                                                  var resp = await this.Api.GetCases(this.User);
                                                  
                                                  // filter some more in the lists
                                                  this.Archived = resp.Where(t => t.IsArchived).ToList();
                                                  this.Open = resp.Where(t => !t.IsArchived).ToList();
                                                  
                                                  // set the collection property to update the API
                                                  this.Cases = new ObservableCollection<Case>(resp);

                                              }
                                              finally
                                                {
                                                  Log ($"Done running {nameof (GetCasesCommand)}");
                                                  this.IsBusy = false;
                                                  this.IsLoading = false;
                                              }
                                          }));
            }
        }

        private ICommand _CaseSelectedCommand;
        /// <summary>
        /// Gets the CaseSelectedCommand.
        /// </summary>
        public ICommand CaseSelectedCommand
        {
            get
            {
                return _CaseSelectedCommand
                    ?? (_CaseSelectedCommand = new Command<Case> (
                                          async (item) =>
                                          {
                                              Log ($"Running {nameof (CaseSelectedCommand)}");
                                              try
                                              {
                                                  // set the case in the vm
                                                  SimpleIoc.Default.GetInstance<CaseDetailsPageViewModel>().Case = item;

                                                  // just navigate to the details page
                                                  await this.Navigation.PushAsync(new CaseDetailsPage());
                                              }
                                              finally
                                              {
                                                  Log ($"Done running {nameof (CaseSelectedCommand)}");
                                              }
                                          }));
            }
        }
    }
}
