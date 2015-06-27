using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem.Messaging
{
    public class Contact
    {
        private String firstName;
        private String lastName;
        private String contactPhone;

        public Contact(String firstName, String lastName, String contactPhone)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.contactPhone = contactPhone;
        }

        public String getFirstName()
        {
            return firstName;
        }

        public String getLastName()
        {
            return lastName;
        }

        public String getContactPhone()
        {
            return contactPhone;
        }

        public void setContactPhone(String contactPhone)
        {
            this.contactPhone = contactPhone;
        }

        public String getContactName()
        {
            return firstName + " " + lastName;
        }

    }

}
