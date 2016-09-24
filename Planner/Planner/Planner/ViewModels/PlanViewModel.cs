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
        ICommand saveCommand, deleteCommand, cancelCommand;

        public PlanViewModel(Plan plan)
        {
            MainText = "New Plan!";
            this.plan = plan;
            saveCommand = new Command(Save);
            deleteCommand = new Command(Delete);
            cancelCommand = new Command(() => Navigation.Pop());
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

        public DateTime StartDate
        {
            get { return plan.startDate; }
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
            get { return saveCommand; }
            set
            {
                if (saveCommand == value)
                    return;
                saveCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteCommand
        {
            get { return deleteCommand; }
            set
            {
                if (deleteCommand == value)
                    return;
                deleteCommand = value;
                OnPropertyChanged();
            }
        }

        public ICommand CancelCommand
        {
            get { return cancelCommand; }
            set
            {
                if (cancelCommand == value)
                    return;
                cancelCommand = value;
                OnPropertyChanged();
            }
        }
    }
}
