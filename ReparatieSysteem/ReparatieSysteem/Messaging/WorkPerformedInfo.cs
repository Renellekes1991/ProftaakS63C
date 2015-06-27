using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReparatieSysteem.Messaging
{
    public class WorkPerformedInfo
    {
        private String description;

        private Int32 price;

        public Int32 Price
        {
            get { return price; }
            set { price = value; }
        }

        public String getDescription
        {
            get { return description; }
        }

        public WorkPerformedInfo(String description)
        {
            this.description = description;
        }
    }
}
