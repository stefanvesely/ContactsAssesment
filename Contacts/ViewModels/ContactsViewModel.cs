using MvvmHelpers;
using MvvmHelpers.Commands;
using Contacts.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Windows.Input;
using Command = MvvmHelpers.Commands.Command;

namespace Contacts.ViewModels
{
    public class ContactsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Contact> ContactList = new ObservableRangeCollection<Contact>();

        public AsyncCommand GetContactsCommand { get; }

        public ContactsViewModel()
        {
            GetContactsCommand = new AsyncCommand(GetContacts);
            FirstLoad();
        }

        public void FirstLoad()
        {
            UpdateContacts();
        }

        public async Task GetContacts()
        {
            IsBusy = true;
            await UpdateContacts();
            IsBusy = false;
        }

        private async Task UpdateContacts()
        {
            ContactList = new ObservableRangeCollection<Contact>(await App.Database.GetContactsAsync());
        }
    }
}