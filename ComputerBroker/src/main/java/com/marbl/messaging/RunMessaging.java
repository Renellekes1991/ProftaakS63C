package com.marbl.messaging;

import com.marbl.computerbroker.ComputerBroker;
import com.marbl.parafiksit.ParafiksitTest;

/**
 * This application tests the LoanBroker system.
 *
 */
public class RunMessaging
{

    public static void main(String[] args)
    {
        try
        {
            // read the queue names from file "MESSAGING.ini"  
            JMSSettings queueNames = new JMSSettings("src/main/resources/MESSAGING_CHANNELS.ini");
            final String factoryName = queueNames.get(JMSSettings.CONNECTION);
            //CLIENTS & BROKER
            final String clientOrderRequestQueue = queueNames.get(JMSSettings.CLIENT_ORDER_REQUEST);
            final String clientOrderReplyQueue = queueNames.get(JMSSettings.CLIENT_ORDER_REPLY);
            final String clientOrder2ReplyQueue = queueNames.get(JMSSettings.CLIENT_ORDER_REPLY_2);
            //PARAFIKSIT & BROKER
            final String parafiksitOrderRequestQueue = queueNames.get(JMSSettings.PARAFIKSIT_ORDER_REQUEST); ///was credit
            final String parafiksitOrderReplyQueue = queueNames.get(JMSSettings.PARAFIKSIT_ORDER_REPLY); //was credit
            //WAREHOUSE & BROKER
            final String warehouseOrderRequestQueue = queueNames.get(JMSSettings.WAREHOUSE_REQUEST);
            final String warehouseOrderReplyQueue = queueNames.get(JMSSettings.WAREHOUSE_REPLY);

            final String warehouseStatusRequestQueue = queueNames.get(JMSSettings.WAREHOUSE_STATUS_REQUEST);
            final String warehouseStatusReplyQueue = queueNames.get(JMSSettings.WAREHOUSE_STATUS_REPLY);

            final String clientStatusRequestQueue = queueNames.get(JMSSettings.CLIENT_STATUS_REQUEST);
            final String clientStatusReplyQueue = queueNames.get(JMSSettings.CLIENT_STATUS_REPLY);
            final String clientStatus2ReplyQueue = queueNames.get(JMSSettings.CLIENT_STATUS_REPLY_2);

            final String parafiksitStatusRequestQueue = queueNames.get(JMSSettings.PARAFIKSIT_STATUS_REQUEST);
            final String parafiksitStatusReplyQueue = queueNames.get(JMSSettings.PARAFIKSIT_STATUS_REPLY);

            ComputerBroker broker = new ComputerBroker(factoryName, clientOrderRequestQueue, parafiksitOrderRequestQueue,
                    parafiksitOrderReplyQueue, warehouseOrderRequestQueue, warehouseOrderReplyQueue, clientStatusRequestQueue,
                    parafiksitStatusRequestQueue, parafiksitStatusReplyQueue);

            //ClientTest client = new ClientTest("basClient", factoryName, clientOrderRequestQueue, clientOrderReplyQueue);

            ParafiksitTest para = new ParafiksitTest(factoryName, parafiksitOrderRequestQueue, parafiksitOrderReplyQueue);

            //WarehouseTest warehouse = new WarehouseTest(factoryName, warehouseOrderRequestQueue, warehouseOrderReplyQueue);

            broker.start();
            //client.start();
            para.start();
            //warehouse.start();

            //client.sendOrderRequest(new ClientOrderRequest("Vanaf=" + client.getClientName()));


            //final String ingRequestQueue = queueNames.get(JMSSettings.BANK_1);
            //final String rabobankRequestQueue = queueNames.get(JMSSettings.BANK_2);
            //final String abnamroRequestQueue = queueNames.get(JMSSettings.BANK_3);
            //final String bankReplyQueue = queueNames.get(JMSSettings.BANK_REPLY);



            // create a ComputerBroker middleware
            //LoanBroker broker = new LoanBroker(factoryName, clientRequestQueue, creditRequestQueue, creditReplyQueue, bankReplyQueue); 
            //broker.addBank(factoryName, ingRequestQueue, ING);
            //broker.addBank(factoryName, rabobankRequestQueue, RABO_BANK);
            //broker.addBank(factoryName, abnamroRequestQueue, ABN_AMRO);


            // create a Client Application
            //LoanTestClient hypotheeker = new LoanTestClient("The Hypotheker", factoryName, clientRequestQueue, clientReplyQueue);
            //LoanTestClient hypotheekvisie = new LoanTestClient("Hypotheekvisie", factoryName, clientRequestQueue, client2ReplyQueue);

            // create the CreditBureau Application
            //CreditBureau creditBureau = new CreditBureau(factoryName, creditRequestQueue, creditReplyQueue);

            // create one Bank application
            //Bank ing = new Bank("ING", factoryName, ingRequestQueue, bankReplyQueue, DEBUG_MODE);
            //Bank rabobank = new Bank("RaboBank", factoryName, rabobankRequestQueue, bankReplyQueue, DEBUG_MODE);
            //Bank abnAmro = new Bank("ABN Amro", factoryName, abnamroRequestQueue, bankReplyQueue, DEBUG_MODE);

            // open all connections in the broker, client and credit applications
            //broker.start();
            //creditBureau.start();
            //ing.start();
            //rabobank.start();
            //abnAmro.start();
            //hypotheeker.start();
            //hypotheekvisie.start(); 

            // send three requests
            //hypotheeker.sendRequest(new ClientOrderRequest(1, 100000, 24));
            //hypotheeker.sendRequest(new ClientOrderRequest(2, 88888, 5));
            //hypotheeker.sendRequest(new ClientOrderRequest(3, 100, 5));

        } catch (Exception ex)
        {
            ex.printStackTrace();
        }

    }
}
