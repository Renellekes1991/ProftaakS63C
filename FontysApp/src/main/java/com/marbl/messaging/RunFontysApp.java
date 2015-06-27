package com.marbl.messaging;

import com.marbl.client.ClientOrderRequest;
import com.marbl.client.ClientMessaging;
import com.marbl.fontysapp.FontysAppFrame;

/**
 * This application tests the LoanBroker system.
 *
 */
public class RunFontysApp
{

    public static void main(String[] args)
    {
        try
        {
            System.out.println("Reading queue names...");
            // read the queue names from file "MESSAGING.ini"  
            JMSSettings queueNames = new JMSSettings("src/main/resources/MESSAGING_CHANNELS.ini");
            
            System.out.println("Done.");
            //CLIENTS & BROKER
            final String factoryName = queueNames.get(JMSSettings.CONNECTION);
            final String clientOrderRequestQueue = queueNames.get(JMSSettings.CLIENT_ORDER_REQUEST);
            final String clientOrderReplyQueue = queueNames.get(JMSSettings.CLIENT_ORDER_REPLY);
            //final String clientOrder2ReplyQueue = queueNames.get(JMSSettings.CLIENT_ORDER_REPLY_2);
//            //PARAFIKSIT & BROKER
//            final String parafiksitOrderRequestQueue = queueNames.get(JMSSettings.PARAFIKSIT_ORDER_REQUEST); ///was credit
//            final String parafiksitOrderReplyQueue = queueNames.get(JMSSettings.PARAFIKSIT_ORDER_REPLY); //was credit
//            //WAREHOUSE & BROKER
//            final String warehouseOrderRequestQueue = queueNames.get(JMSSettings.WAREHOUSE_REQUEST);
//            final String warehouseOrderReplyQueue = queueNames.get(JMSSettings.WAREHOUSE_REPLY);
//
//            final String clientStatusRequestQueue = queueNames.get(JMSSettings.CLIENT_STATUS_REQUEST);
//            final String clientStatusReplyQueue = queueNames.get(JMSSettings.CLIENT_STATUS_REPLY);
//            final String clientStatus2ReplyQueue = queueNames.get(JMSSettings.CLIENT_STATUS_REPLY_2);
//
//            final String parafiksitStatusRequestQueue = queueNames.get(JMSSettings.PARAFIKSIT_STATUS_REQUEST);
//            final String parafiksitStatusReplyQueue = queueNames.get(JMSSettings.PARAFIKSIT_STATUS_REPLY);

            System.out.println("Creating FontysApp GUI...");
            FontysAppFrame frame = new FontysAppFrame(factoryName, clientOrderRequestQueue, clientOrderReplyQueue);
            frame.setVisible(true);
            System.out.println("Done.");
        } catch (Exception ex)
        {
            ex.printStackTrace();
        }
    }
}
