using System;
using System.IO;
using Contacts.Data;
using Contacts.ViewModels;
using Xamarin.Forms;

namespace Contacts
{
    public partial class App : Application
    {
        private static ContactDatabase database;

        public static ContactDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ContactDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Contact.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new ContactPage()) { BarBackground = Color.FromRgb(102, 153, 255), BarTextColor = Color.FromRgb(171, 39, 79) };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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