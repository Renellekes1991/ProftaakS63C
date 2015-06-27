using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem.Messaging
{
    public class ParafiksitOrderReply
    {
        private String nameClient;
        private ShippingAddress address;
        private List<WorkPerformedInfo> workPerformed;
        private Int32 totalPriceForWorkPerformed;
        private String bankAccount;

        public ParafiksitOrderReply() { }

        public ParafiksitOrderReply(String nameClient, ShippingAddress address, List<Ticket> tickets)
        {
            this.nameClient = nameClient;
            this.address = address;

            foreach (Ticket t in tickets)
            {
                WorkPerformedInfo w = new WorkPerformedInfo(t.Probleem);
                w.Price = (int)t.VerwachteKosten;
                totalPriceForWorkPerformed += (int)t.VerwachteKosten;
            }

            bankAccount = "8797701";
        }

        public String getNameClient()
        {
            return nameClient;
        }

        public void setNameClient(String nameClient)
        {
            this.nameClient = nameClient;
        }

        public ShippingAddress getAddress()
        {
            return address;
        }

        public void setAddress(ShippingAddress address)
        {
            this.address = address;
        }

        public List<WorkPerformedInfo> getWorkPerformed()
        {
            return workPerformed;
        }

        public void setWorkPerformed(List<WorkPerformedInfo> workPerformed)
        {
            this.workPerformed = workPerformed;
        }

        public Int32 getTotalPriceForWorkPerformed()
        {
            return totalPriceForWorkPerformed;
        }

        public void setTotalPriceForWorkPerformed()
        {
            this.totalPriceForWorkPerformed = totalPriceForWorkPerformed;
        }

        public String getBankAccount()
        {
            return bankAccount;
        }

        public void setBankAccount(String bankAccount)
        {
            this.bankAccount = bankAccount;
        }
    }
}
