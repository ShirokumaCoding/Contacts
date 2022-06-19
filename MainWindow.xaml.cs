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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Contacts.Models;
using Contacts.Data;

namespace Contacts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Contact> CONTACTS { get; set; } = ContactData.getContacts();
        public static ListBox ListBoxRef;

        public MainWindow()
        {
            InitializeComponent();
            ListBoxRef = this.ContactListBox;
            UpdateContact.IsEnabled = false;
            DeleteContact.IsEnabled = false;

        }

        private void OpenAddContactForm(object sender, RoutedEventArgs e)
        {
            AddContactForm contactForm = new AddContactForm();
            contactForm.Show();
        }

        private void ListBoxItemSelected(object sender, SelectionChangedEventArgs e)
        {
            UpdateContact.IsEnabled = true;
            AddContact.IsEnabled = false;
            DeleteContact.IsEnabled = true;
        }

        private void updateContact(object sender, RoutedEventArgs e)
        {
            // Need to get the value in the listbox
            Contact item = (Contact)ContactListBox.SelectedItem;

            // Pass the object through to the Update Form
            UpdateContactForm updateContactForm = new UpdateContactForm(item);
            updateContactForm.Show();
        }

        private void deleteContact(object sender, RoutedEventArgs e)
        {
            Contact item = (Contact)ContactListBox.SelectedItem;
            string confirmationString = $"Are you sure you want to delete {item.FirstName} {item.LastName}? {Environment.NewLine}This cannot be undone.";
            MessageBoxResult result = MessageBox.Show(confirmationString, "Delete Confirmation",MessageBoxButton.OKCancel);

            // Handle the return value from the MessageBox
            if(result == MessageBoxResult.OK)
            {
                using (var context = new ContactContext())
                {
                    // Query the entity

                    var deleteItem = context.Contacts.Single(e => e.ContactID == item.ContactID);

                    if (deleteItem != null)
                    {
                        context.Contacts.Remove(deleteItem);
                        context.SaveChanges();
                    }
                }
                ListBoxRef.ItemsSource = null;
                ListBoxRef.ItemsSource = ContactData.getContacts();
                AddContact.IsEnabled = true;
            }
        }

        private void ReactivateAddButton(object sender, EventArgs e)
        {
            AddContact.IsEnabled = true;
        }
    }
}
