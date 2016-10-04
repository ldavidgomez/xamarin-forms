using System.Threading.Tasks;
using Android.Content.PM;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

namespace Planner.Droid
{
    [Activity(MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class SplashActivity : Activity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            //Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        protected override void OnResume()
        {
            base.OnResume();

            Task startupWork = new Task(() => {
                //Log.Debug(TAG, "Performing some startup work that takes a bit of time.");
                Task.Delay(5000);  // Simulate a bit of startup work.
                //Log.Debug(TAG, "Working in the background - important stuff.");
            });

            startupWork.ContinueWith(t => {
                //Log.Debug(TAG, "Work is finished - start Activity1.");
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }, TaskScheduler.FromCurrentSynchronizationContext());

            startupWork.Start();
        }
    }

}