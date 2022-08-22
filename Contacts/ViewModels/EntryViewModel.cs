using Contacts.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Command = MvvmHelpers.Commands.Command;

namespace Contacts.ViewModels
{
    public class EntryViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public EntryViewModel(Contact _EntryContact)
        {
            OnValidationCommand = new Command(obj => Validate());

            ContactObject = new ValidationContact()
            {
                Name = _EntryContact.Name,
                ContactNumber = _EntryContact.ContactNumber,
                Email = _EntryContact.Email,
                ID = _EntryContact.ID,
                Date = _EntryContact.Date
            };
            EmailError = true;
            NumberError = true;
            NameError = true;
            Validate();
            if (ContactObject.ID != 0)
            {
                DeleteButton = true;
            }
        }

        private ValidationContact ContactObject { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public string NameErrorText
        {
            get { return ContactObject._Name.NotValidMessageError; }
            set
            {
                ContactObject._Name.NotValidMessageError = value;
                OnPropertyChanged();
            }
        }

        private bool _Save = false;

        public bool SaveEnabled
        {
            get { return _Save; }
            set
            {
                _Save = value;
                OnPropertyChanged();
            }
        }

        private bool _Delete = false;

        public bool DeleteButton
        {
            get { return _Delete; }
            set
            {
                _Delete = value;
                OnPropertyChanged();
            }
        }

        public bool NameError
        {
            get { return ContactObject._Name.IsNotValid; }
            set
            {
                ContactObject._Name.IsNotValid = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return ContactObject.Name; }
            set
            {
                ContactObject.Name = value;
                ValidateName();
                OnPropertyChanged();
            }
        }

        public string NumberErrorText
        {
            get { return ContactObject._ContactNumber.NotValidMessageError; }
            set
            {
                ContactObject._ContactNumber.NotValidMessageError = value;
                OnPropertyChanged();
            }
        }

        public bool NumberError
        {
            get { return ContactObject._ContactNumber.IsNotValid; }
            set
            {
                ContactObject._ContactNumber.IsNotValid = value;
                OnPropertyChanged();
            }
        }

        public string Number
        {
            get { return ContactObject.ContactNumber; }
            set
            {
                ContactObject.ContactNumber = value;
                ValidateNumber();
                OnPropertyChanged();
            }
        }

        public string EmailErrorText
        {
            get { return ContactObject._Email.NotValidMessageError; }
            set
            {
                ContactObject._Email.NotValidMessageError = value;
                OnPropertyChanged();
            }
        }

        public bool EmailError
        {
            get { return ContactObject._Email.IsNotValid; }
            set
            {
                ContactObject._Email.IsNotValid = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return ContactObject.Email; }
            set
            {
                ContactObject.Email = value;
                ValidateEmail();
                OnPropertyChanged();
            }
        }

        public int ID
        {
            get { return ContactObject.ID; }
            set => ContactObject.ID = value;
        }

        public DateTime date
        {
            get { return ContactObject.Date; }
            set => ContactObject.Date = value;
        }

        public ICommand OnValidationCommand { get; set; }

        private async Task Validate()
        {
            await ValidateName();
            await ValidateEmail();
            await ValidateNumber();
        }

        private async Task ValidateEmail()
        {
            if (!Regex.IsMatch(Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                EmailErrorText = "Not a valid email address";
                EmailError = true;
            }
            else
            {
                EmailErrorText = "";
                EmailError = false;
            }

            if (!NameError && !EmailError && !NumberError)
            {
                SaveEnabled = true;
            }
            else
            {
                SaveEnabled = false;
            }
        }

        private async Task ValidateNumber()
        {
            if (!Regex.IsMatch(Number, @"^\d+$", RegexOptions.IgnoreCase))
            {
                //string nNumber = Number.Where(c => char.IsDigit(c)).ToString();
                //Number = nNumber;
                NumberErrorText = "Only Numbers Please";
                NumberError = true;
            }
            else
            {
                NumberErrorText = "";
                NumberError = false;
            }
            if (!NameError && !EmailError && !NumberError)
            {
                SaveEnabled = true;
            }
            else
            {
                SaveEnabled = false;
            }
        }

        private async Task ValidateName()
        {
            if (string.IsNullOrEmpty(Name) || Name == "")
            {
                NameError = true;
                NameErrorText = "You need at least one character for a name";
            }
            else
            {
                NameError = false;
                NameErrorText = "";
            }

            if (!NameError && !EmailError && !NumberError)
            {
                SaveEnabled = true;
            }
            else
            {
                SaveEnabled = false;
            }
        }

        public Contact GetContact()
        {
            return ContactObject;
        }
    }
}