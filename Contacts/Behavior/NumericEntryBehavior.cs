﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Contacts.Behavior
{
    internal class NumericEntryBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.NewTextValue))
            {
                ((Entry)sender).Text = "0";
                return;
            }

            var isValid = args.NewTextValue.ToCharArray().All(char.IsDigit) || (args.NewTextValue.Length > 1 && args.NewTextValue.StartsWith("-")); //Make sure all characters are numbers

            var current = args.NewTextValue;
            current = current.TrimStart('0');

            if (current.Length == 0)
            {
                current = "0";
            }

           ((Entry)sender).Text = isValid ? current : current.Remove(current.Length - 1);
        }
    }
}