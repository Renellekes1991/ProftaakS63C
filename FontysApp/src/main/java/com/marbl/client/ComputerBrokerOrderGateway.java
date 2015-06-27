/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.client;

import com.marbl.messaging.requestreply.AsynchronousRequestor;
import com.marbl.messaging.requestreply.IReplyListener;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
abstract class ComputerBrokerOrderGateway {
    
    private ClientOrderSerializer serializer;
    private AsynchronousRequestor<ClientOrderRequest, ClientOrderReply> gateway;
    
    public ComputerBrokerOrderGateway(String factoryName, String requestQueue, String replyQueue){
        serializer = new ClientOrderSerializer();
        
        try{
            gateway = new AsynchronousRequestor<ClientOrderRequest, ClientOrderReply>(factoryName, requestQueue, replyQueue, serializer);
        } catch (Exception ex) {
            Logger.getLogger(ComputerBrokerOrderGateway.class.getName()).log(Level.SEVERE, null, ex);
        }  
    }
    
    public void order(ClientOrderRequest request){
        System.out.println("Sending request (in computerbrokerordergateway)");
        gateway.sendRequest(request, new IReplyListener<ClientOrderRequest, ClientOrderReply>(){

            public void onReply(ClientOrderRequest request, ClientOrderReply reply) {
                System.out.println("Reply Arrived");
                onOrderReplyArrived(request, reply);
            }
        });
    }
    
    void start(){
        gateway.start();
    }
    
                
    abstract void onOrderReplyArrived(ClientOrderRequest request, ClientOrderReply reply);
}
