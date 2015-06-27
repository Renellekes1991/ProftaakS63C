/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.parafiksit;

import com.marbl.messaging.requestreply.AsynchronousReplier;
import com.marbl.messaging.requestreply.IRequestListener;
import java.util.logging.Level;
import javax.jms.Message;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
abstract class ComputerBrokerOrderGateway {

    private ParafiksitOrderSerializer serializer;
    private AsynchronousReplier<ParafiksitOrderRequest, ParafiksitOrderReply> gateway;

    public ComputerBrokerOrderGateway(String factoryName, String parafiksitRequestQueue, String parafiksitReplyQueue) {
        serializer = new ParafiksitOrderSerializer();

        try {
            gateway = new AsynchronousReplier<ParafiksitOrderRequest, ParafiksitOrderReply>(factoryName, parafiksitRequestQueue, serializer) {
                @Override
                public void beforeSendReply(Message request, Message reply) {
                    //doe niks
                }
            };
        } catch (Exception ex) {
            java.util.logging.Logger.getLogger(com.marbl.parafiksit.ComputerBrokerOrderGateway.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        gateway.setRequestListener(new IRequestListener<ParafiksitOrderRequest>(){

            public void receivedRequest(ParafiksitOrderRequest request) {
                System.out.println("Received parafiksitRequest in parafiksit");
                receivedParafiksitRequest(request);
            }        
        });
    }
    
    abstract void receivedParafiksitRequest(ParafiksitOrderRequest request);
    
    public void start(){
        gateway.start();
    }
    
    void sendReply(ParafiksitOrderRequest request, ParafiksitOrderReply reply){
        System.out.println("Sending reply from parafiksit");
        gateway.sendReply(request, reply);
    }
}
