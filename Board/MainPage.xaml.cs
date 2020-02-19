using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Service;
using Xamarin.Forms;

namespace Board
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public ColorDevice Device;
        public MainPage()
        {
            InitializeComponent();
            this.Device = new ColorDevice("http://192.168.1.25:5000");
            UpdatePackages();
        }

        async public void UpdatePackages()
        {
            installedList.ItemsSource = await this.Device.GetInstalled();
            storeList.ItemsSource = await this.Device.GetRepository();
        }

        async void DeleteButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Result result = await this.Device.DeletePackage((Package)((ImageButton)sender).Parent.BindingContext);
            await DisplayAlert(result.Status == 1 ? "Успех" : "Провал", result.Message, "Понятно");
            UpdatePackages();
        }

        async void InstallButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Result result = await this.Device.InstallPackage((Package)((ImageButton)sender).Parent.BindingContext);
            await DisplayAlert(result.Status == 1 ? "Успех" : "Провал", result.Message, "Понятно");
            UpdatePackages();
        }
    }
}
