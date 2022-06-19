using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.Models;

namespace Contacts.Data
{
    public class ContactData
    {
        public static List<Contact> getContacts()
        {
            var list = new List<Contact>();
            using (var context = new ContactContext())
            {
                foreach (var i in context.Contacts)
                {
                    list.Add(i);
                }
            }
            list = list.OrderBy(item => item.FirstName).ToList();
            return list;
        }
    }
}
