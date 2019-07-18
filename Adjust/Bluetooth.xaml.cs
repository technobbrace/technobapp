﻿using System;
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
    public partial class Bluetooth : ContentPage
    {

        IBluetoothLowEnergyAdapter Adapter { get; set; }
        IBleGattServerConnection GattServer { get; set; }
        Guid serviceGuid = new Guid("0000ffe0-0000-1000-8000-00805f9b34fb");
        Guid sliderGuid = new Guid("0000ffe1-0000-1000-8000-00805f9b34fb");
        Guid buttonGuid = new Guid("ABCD1236-0aaa-467a-9538-01f0652c74e8");

        IDisposable notifyHandler;



        public Bluetooth(IBluetoothLowEnergyAdapter adapter)
        {
            InitializeComponent();
            Adapter = adapter;
        }

        async void btnConnect_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                btnConnect.IsEnabled = false;
                // Forza la connessione
                if (Adapter.AdapterCanBeEnabled && Adapter.CurrentState.IsDisabledOrDisabling())
                {
                    Debug.WriteLine("Attivo adattatore.");
                    await Adapter.EnableAdapter();
                }

                Debug.WriteLine("Tento la connessione.");
                var connection = await Adapter.FindAndConnectToDevice(
                    new ScanFilter()
                        .SetAdvertisedDeviceName("ADJUST")
                        //.SetAdvertisedManufacturerCompanyId(0xffff)
                        //.AddAdvertisedService(guid)
                        ,
                    TimeSpan.FromSeconds(10)
                );


                if (connection.IsSuccessful())
                {
                    await Navigation.PushModalAsync(new MainPage(Adapter, connection));
                }
                else
                {
                    btnConnect.IsEnabled = true;
                    DisplayAlert("Errore", "Device non trovato...", "OK");
                }
            }
            catch (Exception errore)
            {
                DisplayAlert("Errore", errore.Message, "OK");
            }
        }




        void Handle_Appearing(object sender, System.EventArgs e)
        {
            btnConnect.IsEnabled = true;
        }

        async Task Connect(bool connect)
        {
            if (connect)
            {
                // Forza la connessione
                if (Adapter.AdapterCanBeEnabled && Adapter.CurrentState.IsDisabledOrDisabling())
                {
                    Debug.WriteLine("Attivo adattatore.");
                    await Adapter.EnableAdapter();
                }

                Debug.WriteLine("Tento la connessione.");
                var connection = await Adapter.FindAndConnectToDevice(
                    new ScanFilter()
                        .SetAdvertisedDeviceName("ADJUST")
                        //.SetAdvertisedManufacturerCompanyId(0xffff)
                        //.AddAdvertisedService(guid)
                        ,
                    TimeSpan.FromSeconds(30)
                );

                if (connection.IsSuccessful())
                {
                    Debug.WriteLine($"Connesso a { connection.GattServer.DeviceId} { connection.GattServer.Advertisement.DeviceName}");
                    GattServer = connection.GattServer;

                    try
                    {
                        notifyHandler = GattServer.NotifyCharacteristicValue(
                           serviceGuid,
                           buttonGuid,
                           bytes =>
                           {

                               /*// Attento. Qui puòrrivarti un solo byte o due o quattro a seconda del tipo
                                                              // di dato che hai devinito lato ESP32...
                                                              // Ora lato ESP32 ho usato un uint16_t
                                                              var val = BitConverter.ToUInt16(bytes, 0);
                                                              Debug.WriteLine($"{bytes.Count()} byte ({val}) da {buttonGuid}");
                                                              if (val == 1)
                                                                  lblTitolo.BackgroundColor = Color.Red;
                                                              else
                                                                  lblTitolo.BackgroundColor = oldColor;

                                                         */
                           }
                        );
                    }
                    catch (GattException ex)
                    {
                        Debug.WriteLine(ex.ToString());
                    }

                    #region old

                    #endregion

                }
                else
                    Debug.WriteLine("Error connecting to device. result={0:g}", connection.ConnectionResult);
            }
            else
            {
                // Forza la disconnessione
                if (GattServer != null)
                {
                    if (GattServer.State == ConnectionState.Connected ||
                        GattServer.State == ConnectionState.Connecting)
                    {
                        await GattServer.Disconnect();

                        // una volta disconnesso, meglio spegnere anche i notificatori...
                        notifyHandler.Dispose();
                    }
                }
            }

            Debug.WriteLine($"Stato della connessione: { GattServer.State}");
        }
    }
}
