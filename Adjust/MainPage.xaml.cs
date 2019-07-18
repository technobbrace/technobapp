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
using Xamarin.Essentials;

namespace Adjust
{

    [DesignTimeVisible(true)]

   
   
    public partial class MainPage : TabbedPage
    {




        IBluetoothLowEnergyAdapter Adapter { get; set; }
        IBleGattServerConnection GattServer { get; set; }
        BlePeripheralConnectionRequest Connection { get; set; }
        Guid serviceGuid = new Guid("0000ffe0-0000-1000-8000-00805f9b34fb");
        Guid sliderGuid = new Guid("0000ffe1-0000-1000-8000-00805f9b34fb");
        Guid buttonGuid = new Guid("ABCD1236-0aaa-467a-9538-01f0652c74e8");

        IDisposable notifyHandler;



        //public ICommand cmdConnect { get; }
        //public ICommand cmdDisconnect { get; }

        public MainPage(IBluetoothLowEnergyAdapter adapter, BlePeripheralConnectionRequest connection)
        {

            Adapter = adapter;

            Connection = connection;

            // i command cosìon si bindano... Devono essere messi all'interno di una INotifyPropertyChanged...
            //cmdConnect = new Command(async () => await Connect(true));
            //cmdDisconnect = new Command(async () => await Connect(false));

            InitializeComponent();

            Debug.WriteLine($"Connesso a { connection.GattServer.DeviceId}  { connection.GattServer.Advertisement.DeviceName} ");             GattServer = connection.GattServer;              try             {                   Device.StartTimer(TimeSpan.FromSeconds(0.2), () =>                 {                     //AggiornaStato();                     return true; // True = Repeat again, False = Stop the timer                 });              }             catch (GattException ex)             {                 Debug.WriteLine(ex.ToString());             }          


        }



                 void GestioneStatoConnessione(bool connesso)         {            // slValore.IsEnabled = connesso;             /* if (connesso)             {                 //lblTitolo.Text = "ESP32 connesso";                 indicatoreConnessione++;                 if (indicatoreConnessione > 3)                 {                     indicatoreConnessione = 0;                     lblStatoConnesso.TextColor = lblStatoConnesso.TextColor != Color.Red ? Color.Red : Color.Black;                 }             }             else             {                  lblStatoConnesso.TextColor = Color.Black;                 lblTitolo.Text = "ESP32 disconnesso!";             }*/         }






        void OnSettingClicked(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new SettingPage(Adapter,Connection));
        }

     async  void OnBluetoothClicked(object sender, System.EventArgs e)
        {
            // Forza la disconnessione
            if (GattServer != null)             {                 if (GattServer.State == ConnectionState.Connected ||                     GattServer.State == ConnectionState.Connecting)                 {                     await GattServer.Disconnect();                  }             }

            // una volta disconnesso, meglio spegnere anche i notificatori...
            if (notifyHandler != null)                 notifyHandler.Dispose();              await Navigation.PushModalAsync(new Bluetooth(Adapter));         }


    

    async  void OnCervicaleClicked(object sender, System.EventArgs e)
        {

             try                     {                         int sliderValore = Convert.ToInt32(0XFFE0);                                                   // Spedisce il valore dello slider                             byte[] bufferDaSpedire = BitConverter.GetBytes(sliderValore);                              var result = await GattServer.WriteCharacteristicValue(                                 serviceGuid, sliderGuid,                                 bufferDaSpedire                             );                                              }                     catch (Exception errore)                     {                         Debug.WriteLine(errore.ToString());                     } 
        }




     async   void OnLombareClicked(object sender, System.EventArgs e)
        {

            try             {                 int sliderValore = Convert.ToInt32(0XFFE1);                    // Spedisce il valore dello slider                 byte[] bufferDaSpedire = BitConverter.GetBytes(sliderValore);                  var result = await GattServer.WriteCharacteristicValue(                     serviceGuid, sliderGuid,                     bufferDaSpedire                 );              }             catch (Exception errore)             {                 Debug.WriteLine(errore.ToString());             }
        }

        void OnSalvaClicked(object sender, System.EventArgs e)
        {

            string text = name.Text;

            ename.Text = text;

            DisplayAlert("Successo", "Nome salvato", "OK");

           

        }

        void OnEmailClicked(object sender, System.EventArgs e)
        {
            

            

            var message = new EmailMessage
            {

                Subject = "Da inviare a technobackbrace@gmail.com",
                Body = editorEmail.Text,
                
            };
             Email.ComposeAsync(message);

        }

        void OnFbClicked(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://it-it.facebook.com/technobackbrace/"));
        }

        void OnInstaClicked(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.instagram.com/technobackbrace/"));
        }


    }
}
