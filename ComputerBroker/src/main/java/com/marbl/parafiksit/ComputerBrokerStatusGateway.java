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
abstract class ComputerBrokerStatusGateway {

    private ParafiksitStatusSerializer serializer;
    private AsynchronousReplier<ParafiksitStatusRequest, ParafiksitStatusReply> gateway;

    public ComputerBrokerStatusGateway(String factoryName, String parafiksitRequestQueue, String parafiksitReplyQueue) {
        serializer = new ParafiksitStatusSerializer();

        try {
            gateway = new AsynchronousReplier<ParafiksitStatusRequest, ParafiksitStatusReply>(factoryName, parafiksitRequestQueue, serializer) {
                @Override
                public void beforeSendReply(Message request, Message reply) {
                    throw new UnsupportedOperationException("Not supported yet.");
                }
            };
        } catch (Exception ex) {
            java.util.logging.Logger.getLogger(com.marbl.parafiksit.ComputerBrokerStatusGateway.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        gateway.setRequestListener(new IRequestListener<ParafiksitStatusRequest>(){

            public void receivedRequest(ParafiksitStatusRequest request) {
                receivedParafiksitRequest(request);
            }        
        });
    }
    
    abstract void receivedParafiksitRequest(ParafiksitStatusRequest request);
    
    public void start(){
        gateway.start();
    }
    
    void sendReply(ParafiksitStatusRequest request, ParafiksitStatusReply reply){
        gateway.sendReply(request, reply);
    }
}
