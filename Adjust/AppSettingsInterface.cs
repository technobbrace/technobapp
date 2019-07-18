using System;

using Xamarin.Forms;
using static Adjust.MainPage;

namespace Adjust
{
    public class AppSettingsInterface : ContentPage
    {
        public AppSettingsInterface : IAppSettingsHelper
        {

    {
                public void OpenAppSettings()
                {
                    var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
                    intent.AddFlags(ActivityFlags.NewTask);
                    string package_name = "technob";
                    var uri = Android.Net.Uri.FromParts("package", package_name, null);
                    intent.SetData(uri);
                    Application.Context.StartActivity(intent);
                }

            }
        }
    }
}

