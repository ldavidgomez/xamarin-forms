﻿using Planner.Model;
using Planner.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace Planner.ViewModels
{
    class PlanViewModel : BaseViewModel
    {
        Plan plan;
		DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
		ICommand _saveCommand, _deleteCommand, _cancelCommand;

        const string dateFormatToPersist = "yyyy-MM-dd HH:mm:ss";
        const string dateFormat = "dd/MM/yy";

        public PlanViewModel(Plan plan)
        {
            MainText = "New Plan!";
            this.plan = plan;
			SaveCommand = new Command(Save);
            DeleteCommand = new Command(Delete);
            CancelCommand = new Command(() => Navigation.Pop());
        }

        public Plan Plan { get { return plan; } }

        public void Save()
        {
            MessagingCenter.Send(this, "PlanSaved", plan);
            Navigation.Pop();
        }
        public void Delete()
        {
            MessagingCenter.Send(this, "PlanDeleted", plan);
            Navigation.Pop();
        }


        private string _mainText;
        public string MainText
        {
            get { return _mainText; }
            private set { _mainText = value; }
        }

        public string Description
        {
            get { return plan.description; }
            set
            {
                if (plan.description == value)
                    return;
                plan.description = value;
                OnPropertyChanged();
            }
        }

        public string Category
        {
            get { return plan.category; }
            set
            {
                if (plan.category == value)
                    return;
                plan.category = value;
                OnPropertyChanged();
            }
        }

		ObservableCollection<string> _categories = new ObservableCollection<string>();
		public ObservableCollection<string> Categories
		{
			get
			{
				if (_categories.Count == 0)
					_categories.Add("nothing to do here!");
				return _categories;
			}
			set
			{
				if (_categories == value)
					return;
				_categories = value;
				OnPropertyChanged();
			}
		}

		bool _categoriesVisibility = false;
		public bool CategoriesVisibility
		{
			get { return _categoriesVisibility; }
			set { _categoriesVisibility = value; }
		}

		public DateTime StartDate
		{
			get { return DateTime.Parse(plan.startDate); }
			set
			{
				if (plan.startDate == value.Date.ToString(dateFormat))
					return;

				plan.startDate = value.ToString(dateFormatToPersist);
				OnPropertyChanged();
			}
		}

		public DateTime MinimunDate
		{
			get { return DateTimeUtils.StartOfWeek(DateTime.Parse(plan.startDate), dfi.FirstDayOfWeek); }
		}

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			switch (propertyName)
			{
				case "Category":
					SetCategories(Category);
					break;
				default:
					base.OnPropertyChanged(propertyName);
					break;
			}
		}

		private void SetCategories(string category)
		{
			var categories = App.Database.GetCategories(category);

			var x = new ObservableCollection<string>();
			foreach (var t in categories)
			{
				x.Add(t);
			}
			Categories = x;

			CategoriesVisibility = true;
		}

		public ICommand SaveCommand
        {
            get { return _saveCommand; }
            set
            {
                if (_saveCommand == value)
                    return;
                _saveCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand
        {
            get { return _deleteCommand; }
            set
            {
                if (_deleteCommand == value)
                    return;
                _deleteCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand CancelCommand
        {
            get { return _cancelCommand; }
            set
            {
                if (_cancelCommand == value)
                    return;
                _cancelCommand = value;
                OnPropertyChanged();
            }
        }

        object selectedPlan;
        public object SelectedPlan
        {
            get { return selectedPlan; }
            set
            {
                if (selectedPlan == value)
                    return;
                // something was selected
                selectedPlan = value;

                OnPropertyChanged();

                if (selectedPlan != null)
                {

                    var todovm = new PlanViewModel(((PlanViewModel)selectedPlan).Plan);

                    Navigation.Push(ViewFactory.CreatePage(todovm));

                    selectedPlan = null;
                }
            }
        }
	}
}
