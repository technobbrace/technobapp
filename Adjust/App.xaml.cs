using System;
using nexus.protocols.ble;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;

namespace Adjust
{
    public partial class App : Application
    {
        public App(IBluetoothLowEnergyAdapter adapter)
        {
            InitializeComponent();

            MainPage = new Bluetooth(adapter);

            
        }

        protected override void OnStart()
        {
            AppCenter.Start("10634f37-fb7b-4dea-a0a6-2b625cc0a831", typeof(Push));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
