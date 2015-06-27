using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem.Messaging
{
    public class ParafiksitOrderRequest
    {
        private ShippingAddress shippingAddress;
        private Contact contact;
        private List<WorkPerformedInfo> workPerformed;

        public ParafiksitOrderRequest() { }

        public ParafiksitOrderRequest(List<WorkPerformedInfo> work, Contact contact, ShippingAddress shipping)
        {
            this.workPerformed = work;
            this.contact = contact;
            this.shippingAddress = shipping;
        }

        public ShippingAddress getShippingAddress()
        {
            return shippingAddress;
        }

        public Contact getContact()
        {
            return contact;
        }

        public List<WorkPerformedInfo> getWorkPerformed()
        {
            return workPerformed;
        }

    }
}
