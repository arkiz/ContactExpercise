using Android.App;
using Android.Widget;
using Android.OS;

namespace ContactExpercise
{
    [Activity(Label = "ContactExpercise", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var contactAdapter = new ContactAdapter(this);
            var contactListview = FindViewById<ListView>(Resource.Id.contactListView);

            contactListview.Adapter = contactAdapter;


        }
    }
}

