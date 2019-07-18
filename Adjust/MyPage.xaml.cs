using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Adjust
{
    public partial class MyPage : ContentPage
    {
        public MyPage()
        {
            InitializeComponent();


        }

        void OnAccendiClicked(object sender, System.EventArgs e)
        {
        }

        void OnSpegniClicked(object sender, System.EventArgs e)
        {
        }

        void OnPositionClicked(object sender, System.EventArgs e)
        {
        }

        void OnHomeClicked(object sender, System.EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }


    }
}
