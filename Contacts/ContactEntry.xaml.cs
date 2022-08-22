using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Data;
using Contacts.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using SQLite;
using Contacts.ViewModels;

namespace Contacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactEntry : ContentPage
    {
        private EntryViewModel contact;

        public ContactEntry(Contact _contact)
        {
            InitializeComponent();
            contact = new EntryViewModel(_contact);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = contact;
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            await App.Database.SaveContactAsync(new Contact()
            {
                ContactNumber = contact.Number,
                Name = contact.Name,
                Email = contact.Email,
                ID = contact.ID,
                Date = DateTime.Now
            });
            await Navigation.PopAsync(true);
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            await App.Database.DeleteContactAsync(new Contact()
            {
                ContactNumber = contact.Number,
                Name = contact.Name,
                Email = contact.Email,
                ID = contact.ID,
                Date = DateTime.Now
            });
            await Navigation.PopAsync(true);
        }
    }
}