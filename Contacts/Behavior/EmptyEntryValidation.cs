using Contacts.Controls;
using Xamarin.Forms;

namespace Contacts.Behavior
{
    public class EmptyEntryValidation : Behavior<ExtendedEntry>
    {
        private ExtendedEntry control;
        private string placeholder;

        private Color _placeholdercolour;

        protected override void OnAttachedTo(ExtendedEntry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                ((ExtendedEntry)sender).IsBorderErrorVisible = false;
            }
        }

        protected override void OnDetachingFrom(ExtendedEntry bindable)
        {
            bindable.PropertyChanged += OnPropertyChanged;
            control = bindable;
            placeholder = bindable.Placeholder;
            _placeholdercolour = bindable.PlaceholderColor;
        }

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ExtendedEntry.IsBorderErrorVisibleProperty.PropertyName && control != null)
            {
                if (control.IsBorderErrorVisible)
                {
                    control.Placeholder = control.ErrorText;
                    control.PlaceholderColor = control.BorderErrorColor;
                    control.Text = string.Empty;
                }
                else
                {
                    control.Placeholder = placeholder;
                    control.PlaceholderColor = _placeholdercolour;
                }
            }
        }
    }
}