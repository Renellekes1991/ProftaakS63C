/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.client;

import com.marbl.messaging.requestreply.AsynchronousRequestor;
import com.marbl.messaging.requestreply.IReplyListener;
import java.util.logging.Level;
import java.util.logging.Logger;

abstract class ComputerBrokerStatusGateway {
    
    private ClientStatusSerializer serializer;
    private AsynchronousRequestor<ClientStatusRequest, ClientStatusReply> gateway;
    
    public ComputerBrokerStatusGateway(String factoryName, String requestQueue, String replyQueue){
        serializer = new ClientStatusSerializer();
        
        try{
            gateway = new AsynchronousRequestor<ClientStatusRequest, ClientStatusReply>(factoryName, requestQueue, replyQueue, serializer);
        } catch (Exception ex) {
            Logger.getLogger(ComputerBrokerStatusGateway.class.getName()).log(Level.SEVERE, null, ex);
        }  
    }
    
    public void getStatus(ClientStatusRequest request){
        gateway.sendRequest(request, new IReplyListener<ClientStatusRequest, ClientStatusReply>(){

            public void onReply(ClientStatusRequest request, ClientStatusReply reply) {
                onStatusReplyArrived(request, reply);
            }
        });
    }
    
    void start(){
        gateway.start();
    }
    
                
    abstract void onStatusReplyArrived(ClientStatusRequest request, ClientStatusReply reply);
}
