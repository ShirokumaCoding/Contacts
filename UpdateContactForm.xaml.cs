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
    
    public partial class UpdateContactForm : Window
    {

        public int UpdateContactID { get; }

        public UpdateContactForm(Contact contact)
        {
            InitializeComponent();
            UpdateFirstNameTextBox.Text = contact.FirstName;
            UpdateLastNameTextBox.Text = contact.LastName;
            UpdatePhoneNumberTextBox.Text = contact.PhoneNumber;
            UpdateContactID = contact.ContactID;
        }

        private void UpdateFormClose(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateClearFields(object sender, RoutedEventArgs e)
        {
            UClear();
        }

        private void updateContact(object sender, RoutedEventArgs e)
        {
            using (var context = new ContactContext())
            {
                // Query the entity

                var updateItem = context.Contacts.Single(e => e.ContactID == UpdateContactID);

                if (updateItem != null)
                {
                    updateItem.FirstName = UpdateFirstNameTextBox.Text;
                    updateItem.LastName = UpdateLastNameTextBox.Text;
                    updateItem.PhoneNumber = UpdatePhoneNumberTextBox.Text;
                }

                context.Contacts.Update(updateItem);
                context.SaveChanges();

            }
            UClear();
            MainWindow.ListBoxRef.ItemsSource = null;
            MainWindow.ListBoxRef.ItemsSource = ContactData.getContacts();
            this.Close();
        }

        public void UClear()
        {
            UpdateFirstNameTextBox.Text = string.Empty;
            UpdateLastNameTextBox.Text = string.Empty;
            UpdatePhoneNumberTextBox.Text = string.Empty;
            UpdateFirstNameTextBox.Focus();
        }
    }
}
