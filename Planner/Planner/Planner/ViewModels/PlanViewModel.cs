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

        public Plan Plan { get { return plan; } }

        private string _mainText;
        public string MainText
        {
            get { return _mainText; }
            private set { _mainText = value; }
        }

        public PlanViewModel(Plan plan)
        {
            MainText = "New Plan!";
            this.plan = plan;
            saveCommand = new Command(Save);
            deleteCommand = new Command(Delete);
            cancelCommand = new Command(() => Navigation.Pop());
        }
        public void Save()
        {
            MessagingCenter.Send(this, "TodoSaved", plan);
            Navigation.Pop();
        }
        public void Delete()
        {
            MessagingCenter.Send(this, "TodoDeleted", plan);
            Navigation.Pop();
        }
    }
}
