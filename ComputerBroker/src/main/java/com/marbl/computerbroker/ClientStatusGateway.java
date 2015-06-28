/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.computerbroker;

import com.marbl.client.ClientOrderReply;
import com.marbl.client.ClientOrderRequest;
import com.marbl.client.ClientOrderSerializer;
import com.marbl.client.ClientStatusReply;
import com.marbl.client.ClientStatusRequest;
import com.marbl.client.ClientStatusSerializer;
import com.marbl.messaging.requestreply.AsynchronousReplier;
import com.marbl.messaging.requestreply.IRequestListener;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.jms.Message;


abstract class ClientStatusGateway {

    private ClientStatusSerializer serializer;
    private AsynchronousReplier<ClientStatusRequest, ClientStatusReply> gateway;

    public ClientStatusGateway(String factoryName, String clientRequestQueue) {
        serializer = new ClientStatusSerializer();

        try {
            gateway = new AsynchronousReplier<ClientStatusRequest, ClientStatusReply>(factoryName, clientRequestQueue, serializer) {
                @Override
                public void beforeSendReply(Message request, Message reply) {
                    //????
                }
            };
        } catch (Exception ex) {
            Logger.getLogger(ClientStatusGateway.class.getName()).log(Level.SEVERE, null, ex);
        }

        gateway.setRequestListener(new IRequestListener<ClientStatusRequest>() {

            public void receivedRequest(ClientStatusRequest request) {
                onClientRequest(request);
            }
        });
    }
    
    abstract void onClientRequest(ClientStatusRequest request);
    
    void start(){
        gateway.start();
    }
    
    void sendReply(ClientStatusRequest request, ClientStatusReply reply){
        gateway.sendReply(request, reply);
    }
}
