    using Planner.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Planner.ViewModels
{
    class PlanViewModel : BaseViewModel
    {
        Plan plan;
        ICommand _saveCommand, _deleteCommand, _cancelCommand;

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

        public string StartDate
        {
            get { return DateTime.Parse(plan.startDate).Date.ToString(dateFormat); }
            set
            {
                if (plan.startDate == value)
                    return;
                plan.startDate = value;
                OnPropertyChanged();
            }
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
