using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Contacts.Models
{
    internal class ValidationContact : Contact, INotifyPropertyChanged
    {
        public Field _Name { get; set; } = new Field();
        public Field _Email { get; set; } = new Field();
        public Field _ContactNumber { get; set; } = new Field();

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Field : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public bool IsNotValid { get; set; }
        public string NotValidMessageError { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}