using System;
using System.Collections.Generic;
using Board.Service;
using Xamarin.Forms;

namespace Board.Pages
{
    public partial class PackageDetailsPage : ContentPage
    {
        public PackageDetailsPage(Package package)
        {
            InitializeComponent();
            this.BindingContext = package;
        }
    }
}
