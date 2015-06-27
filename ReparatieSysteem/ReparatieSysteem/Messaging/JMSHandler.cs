using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Spring.Messaging.Nms;
using Spring.Messaging.Nms.Listener;
using Spring.Messaging.Nms.Core;
using Spring.Messaging.Nms.Support.Destinations;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace ReparatieSysteem.Messaging
{
    class JMSHandler : IMessageListener
    {
        private const string URI = "tcp://localhost:3700"; //3700 of 7676
        private const string DESTINATION_REQUEST = "parafiksitOrderRequestQueue";
        private const string DESTINATION_REPLY = "parafiksitOrderReplyQueue";
        private ConnectionFactory connectionFactory;
        private ReparatieSysteem reparatieSysteem;
        private OracleDatabase database;

        public JMSHandler(ReparatieSysteem reparatieSysteem)
        {
            this.reparatieSysteem = reparatieSysteem;
            database = new OracleDatabase();

            connectionFactory = new ConnectionFactory(URI);
            connectionFactory.ClientId = "Parafixit";
            //template = new NmsTemplate(connectionFactory);

            SimpleMessageListenerContainer listenerContainer = new SimpleMessageListenerContainer();
            
            listenerContainer.ConnectionFactory = connectionFactory;
            listenerContainer.DestinationName = DESTINATION_REQUEST;
            listenerContainer.MessageListener = this;
            listenerContainer.AfterPropertiesSet();

            XmlSerializer serializer = new XmlSerializer(typeof(ParafiksitOrderRequest));
            Console.WriteLine(serializer);
        }

        public void Send(ParafiksitOrderReply message)
        {
            try
            {
                IConnection conn = connectionFactory.CreateConnection();
                conn.Start();
                ISession sess = conn.CreateSession();
                NmsDestinationAccessor destinationResolver = new NmsDestinationAccessor();

                IDestination dest = destinationResolver.ResolveDestinationName(sess, DESTINATION_REPLY);
                IMessageProducer prod = sess.CreateProducer(dest);
               
                XmlSerializer serializer = new XmlSerializer(typeof(ParafiksitOrderReply));

                StringWriter stringWriter = new StringWriter();
                XmlWriter writer = XmlWriter.Create(stringWriter);
                serializer.Serialize(writer, message);
                var xml = stringWriter.ToString();

                ITextMessage textMessage = prod.CreateTextMessage(xml);
                
                prod.Send(textMessage);
                Console.WriteLine("Sent: " + message);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }


        public void OnMessage(IMessage message)
        {
            try
            {
                ITextMessage textMessage = message as ITextMessage;

                Console.WriteLine("Received: " + textMessage.Text);

                //var knownTypes = new Type[] { typeof(Contact), typeof(ShippingAddress), typeof(WorkPerformedInfo)};

                XmlSerializer serializer = new XmlSerializer(typeof(ParafiksitOrderRequest));//, knownTypes);

                ParafiksitOrderRequest request = (ParafiksitOrderRequest)serializer.Deserialize(new System.Xml.XmlTextReader(new System.IO.StringReader(textMessage.Text)));

                Console.WriteLine("Name: " + request.getContact().getFirstName());

                //createReply(request);

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public void createReply(ParafiksitOrderRequest request)
        {
            Contact contact = request.getContact();
            ShippingAddress address = request.getShippingAddress();
            int number = 0;
            //Int32.TryParse(address.getNumber(), out number);

            Klant klant = reparatieSysteem.VoegKlantToe(contact.getFirstName(), "", contact.getLastName(), address.getPlace(), address.getPostalCode(), address.getStreet(), number, "", "0", "Nederland");
            int klantNummer = 0;

            foreach (Klant k in database.GetKlanten())
            {
                // Omslachtig om nummer op te halen had beter gekunt
                if (k.Achternaam == klant.Achternaam && k.Postcode == klant.Postcode && k.Huisnummer == klant.Huisnummer)
                {
                    klantNummer = k.Nr;
                }
            }

            Random random = new Random();
            List<Ticket> tickets = new List<Ticket>();

            foreach (WorkPerformedInfo w in request.getWorkPerformed())
            {
                Ticket t = reparatieSysteem.VoegTicketToe(klantNummer, (random.NextDouble() * (100.0 - 0.0) + 0.0), random.Next(0,100), "Repa", w.getDescription, "repa");
                tickets.Add(t);
            }

            ParafiksitOrderReply reply = new ParafiksitOrderReply(klant.Voornaam, address, tickets);
            Send(reply);
        }
    }
}
