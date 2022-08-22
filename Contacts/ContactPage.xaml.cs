using Contacts.Models;
using Contacts.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contacts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {
        private ContactsViewModel Contactsvm;

        public ContactPage()
        {
            InitializeComponent();
            Contactsvm = new ContactsViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            bool isrunning = true;
            await Contactsvm.GetContacts();
            collectionview.ItemsSource = Contactsvm.ContactList;
            collectionview.SelectedItem = null;
        }

        private async void OnNoteAddedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactEntry(new Contact())
            {
            });
        }

        private async void OnItemSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() != null)
            {
                await Navigation.PushAsync(new ContactEntry(e.CurrentSelection.FirstOrDefault() as Contact)
                {
                });
            }
        }
    }
}