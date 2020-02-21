using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Package> installed = new ObservableCollection<Package>();
        ObservableCollection<Package> store = new ObservableCollection<Package>();
        ViewCell runningAppCell = null;

        public MainPage()
        {
            InitializeComponent();
            this.Device = new ColorDevice("http://192.168.1.25:5000");
            UpdatePackages();
        }

        async public void UpdatePackages()
        {
            installed.Clear();
            store.Clear();

            List<string> installedIdentifiers = new List<string>();
            foreach (Package package in await this.Device.GetInstalled())
            {
                installedIdentifiers.Add(package.Identifier);
                installed.Add(package);
            }
                

            foreach (Package package in await this.Device.GetRepository())
            {
                if (!installedIdentifiers.Contains(package.Identifier))
                {
                    store.Add(package);
                }
            }

            installedList.ItemsSource = installed;
            storeList.ItemsSource = store;
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

        async void PackageDetails_Tapped(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Pages.PackageDetailsPage((Package)((ViewCell)sender).BindingContext));
        }

        async void OpenPackage_Tapped(System.Object sender, System.EventArgs e)
        {
            Result result = await this.Device.OpenPackage((Package)((ViewCell)sender).BindingContext);
            
            
            if (result.Status != 1)
            {
                await DisplayAlert("Провал", result.Message, "Понятно");
            }
            else
            {
                if (runningAppCell != null)
                    runningAppCell.View.BackgroundColor = Color.White;
                runningAppCell = (ViewCell)sender;
                runningAppCell.View.BackgroundColor = Color.LightGreen;
            }
        }
    }
}
