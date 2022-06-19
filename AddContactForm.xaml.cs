using Contacts.Data;
using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Contacts
{
    /// <summary>
    /// Interaction logic for AddContactForm.xaml
    /// </summary>
    public partial class AddContactForm : Window
    {
        public AddContactForm()
        {
            InitializeComponent();
        }

        private void CloseAddContactForm(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClearFields(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void AddContact(object sender, RoutedEventArgs e)
        {
            using(var context = new ContactContext())
            {
                Contact newContact = new Contact()
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    PhoneNumber = PhoneNumberTextBox.Text
                };
                context.Contacts.Add(newContact);
                context.SaveChanges();
            
            }
            Clear();
            MainWindow.ListBoxRef.ItemsSource = null;
            MainWindow.ListBoxRef.ItemsSource = ContactData.getContacts();
        }

        public void Clear()
        {
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
            FirstNameTextBox.Focus();
        }

        private void AddContactAndClose(object sender, RoutedEventArgs e)
        {
            AddContact(sender, e);
            CloseAddContactForm(sender, e);
        }
    }
}
