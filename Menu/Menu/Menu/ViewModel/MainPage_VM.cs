using Menu.Model;
using Menu.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Menu.ViewModel
{
    public class MainPage_VM : BaseViewModel
    {
        private static IPlanService PlanService { get; } = DependencyService.Get<IPlanService>();

        private string _mainText;
        public string MainText
        {
            get { return _mainText; }
            private set { SetProperty (ref _mainText, value); }
        }

        private IList<Plan> _lastUpdatePlans;
        public IList<Plan> LastUpdatePlans
        {
            get { return _lastUpdatePlans; }
            private set { SetProperty(ref _lastUpdatePlans, value); }
        }

        private string _categoryText;
        public string CategoryText
        {
            get { return _categoryText; }
            private set { SetProperty(ref _categoryText, value); }
        }

        private IList<Plan> _categories;
        public IList<Plan> Categories
        {
            get { return _categories; }
            private set { SetProperty(ref _categories, value); }
        }

        public MainPage_VM(Page page)
        {
            page.Appearing += (sender, e) =>
            {
                Bind_MainText();
                Bind_LastUpdatePlans();
                Bind_CategoryText();
                Bind_Categories();
            };
        }

        private void Bind_MainText()
        {
            MainText = "Planner!";
        }

        private void Bind_LastUpdatePlans()
        {
            LastUpdatePlans = PlanService.GetLastUpdates();
        }
        private void Bind_CategoryText()
        {
            CategoryText = "Categories!";
        }

        private void Bind_Categories()
        {
            Categories = PlanService.GetCategories();
        }
    }
}
