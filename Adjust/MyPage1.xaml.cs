using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using nexus.protocols.ble;
using nexus.protocols.ble.scan;
using Xamarin.Forms;
using nexus.protocols.ble.gatt.adopted;
using nexus.protocols.ble.gatt;

namespace Adjust
{
    public partial class MyPage1 : ContentPage
    

    {

        IBluetoothLowEnergyAdapter Adapter
        { get; set; }
        IBleGattServerConnection GattServer { get; set; }
        BlePeripheralConnectionRequest Connection { get; set; }
        Guid serviceGuid = new Guid("0000ffe0-0000-1000-8000-00805f9b34fb");
        Guid sliderGuid = new Guid("0000ffe1-0000-1000-8000-00805f9b34fb");
        Guid buttonGuid = new Guid("ABCD1236-0aaa-467a-9538-01f0652c74e8");

        IDisposable notifyHandler;


        public MyPage1( IBluetoothLowEnergyAdapter adapter, BlePeripheralConnectionRequest connection)
        {
            InitializeComponent();

            Adapter = adapter;

            Connection = connection;

           //valpower.Text = String.Format("{0:F1}", power.Value);


            Debug.WriteLine($"Connesso a { connection.GattServer.DeviceId}  { connection.GattServer.Advertisement.DeviceName} ");             GattServer = connection.GattServer;              try             {                   Device.StartTimer(TimeSpan.FromSeconds(0.2), () =>                 {                     //AggiornaStato();                     return true; // True = Repeat again, False = Stop the timer                 });              }             catch (GattException ex)             {                 Debug.WriteLine(ex.ToString());             } 
        }

        int potenza;

      async void OnAccendiClicked(object sender, System.EventArgs e)
        {

            try             {                 int sliderValore = Convert.ToInt32(22222222);                    // Spedisce il valore dello slider                 byte[] bufferDaSpedire = BitConverter.GetBytes(sliderValore);                  var result = await GattServer.WriteCharacteristicValue(                     serviceGuid, sliderGuid,                     bufferDaSpedire                 );              }             catch (Exception errore)             {                 Debug.WriteLine(errore.ToString());             } 

        }

      async  void OnSpegniClicked(object sender, System.EventArgs e)
        {

            try             {                 int sliderValore = Convert.ToInt32(33333333);                    // Spedisce il valore dello slider                 byte[] bufferDaSpedire = BitConverter.GetBytes(sliderValore);                  var result = await GattServer.WriteCharacteristicValue(                     serviceGuid, sliderGuid,                     bufferDaSpedire                 );              }             catch (Exception errore)             {                 Debug.WriteLine(errore.ToString());             } 

        }

     async  void OnPositionClicked(object sender, System.EventArgs e)
        {

             try             {                 int sliderValore = Convert.ToInt32(66666666);                    // Spedisce il valore dello slider                 byte[] bufferDaSpedire = BitConverter.GetBytes(sliderValore);                  var result = await GattServer.WriteCharacteristicValue(                     serviceGuid, sliderGuid,                     bufferDaSpedire                 );              }             catch (Exception errore)             {                 Debug.WriteLine(errore.ToString());             } 

        }


        async void OnSuClicked(object sender, System.EventArgs e)         {              potenza = potenza + 1;              try             {                 int sliderValore = Convert.ToInt32(77777777);                    // Spedisce il valore dello slider                 byte[] bufferDaSpedire = BitConverter.GetBytes(sliderValore);                  var result = await GattServer.WriteCharacteristicValue(                     serviceGuid, sliderGuid,                     bufferDaSpedire                 );              }             catch (Exception errore)             {                 Debug.WriteLine(errore.ToString());             }         }          async void OnGiuClicked(object sender, System.EventArgs e)         {              potenza = potenza - 1;              try             {                 int sliderValore = Convert.ToInt32(44444444);                    // Spedisce il valore dello slider                 byte[] bufferDaSpedire = BitConverter.GetBytes(sliderValore);                  var result = await GattServer.WriteCharacteristicValue(                     serviceGuid, sliderGuid,                     bufferDaSpedire                 );              }             catch (Exception errore)             {                 Debug.WriteLine(errore.ToString());             }         } 



        void OnHomeClicked(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage(Adapter, Connection));
        }


    }
}
