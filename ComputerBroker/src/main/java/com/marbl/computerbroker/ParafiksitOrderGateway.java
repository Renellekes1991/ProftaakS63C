/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.computerbroker;

import com.marbl.messaging.requestreply.AMQAsynchronousRequestor;
import com.marbl.messaging.requestreply.IReplyListener;
import com.marbl.parafiksit.ParafiksitOrderReply;
import com.marbl.parafiksit.ParafiksitOrderRequest;
import com.marbl.parafiksit.ParafiksitOrderSerializer;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.jms.JMSException;


abstract class ParafiksitOrderGateway {
    private ParafiksitOrderSerializer serializer;
    private AMQAsynchronousRequestor<ParafiksitOrderRequest, ParafiksitOrderReply> gateway;
    
    public ParafiksitOrderGateway(String factoryName, String parafiksitRequestQueue, String parafiksitReplyQueue)
    {
        serializer = new ParafiksitOrderSerializer();
        try {
            gateway = new AMQAsynchronousRequestor<ParafiksitOrderRequest, ParafiksitOrderReply>(factoryName, parafiksitRequestQueue, parafiksitReplyQueue, serializer);
        } catch (Exception ex) {
            Logger.getLogger(ParafiksitOrderGateway.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    public void start(){
        try
        {
            gateway.start();
        }
        catch(Exception ex)
        {
            Logger.getLogger(ParafiksitOrderGateway.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    public void registerOrder(ParafiksitOrderRequest request, IReplyListener<ParafiksitOrderRequest, ParafiksitOrderReply> listener){
        System.out.println("Sending request from broker to parafiksit");
        gateway.sendRequest(request, listener);
    }
}
