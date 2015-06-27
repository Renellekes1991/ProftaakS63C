/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.computerbroker;


import com.marbl.client.ClientOrderReply;
import com.marbl.client.ClientOrderRequest;
import com.marbl.client.ClientOrderSerializer;
import com.marbl.messaging.requestreply.AsynchronousReplier;
import com.marbl.messaging.requestreply.IRequestListener;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.jms.Message;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
abstract class ClientOrderGateway {

    private ClientOrderSerializer serializer;
    private AsynchronousReplier<ClientOrderRequest, ClientOrderReply> gateway;

    public ClientOrderGateway(String factoryName, String clientRequestQueue) {
        serializer = new ClientOrderSerializer();

        try {
            gateway = new AsynchronousReplier<ClientOrderRequest, ClientOrderReply>(factoryName, clientRequestQueue, serializer) {
                @Override
                public void beforeSendReply(Message request, Message reply) {
                    //????
                }
            };
        } catch (Exception ex) {
            Logger.getLogger(ClientOrderGateway.class.getName()).log(Level.SEVERE, null, ex);
        }

        gateway.setRequestListener(new IRequestListener<ClientOrderRequest>() {

            @Override
            public void receivedRequest(ClientOrderRequest request) {
                System.out.println("Broker received request from client");
                onClientRequest(request);
            }
        });
    }
    
    abstract void onClientRequest(ClientOrderRequest request);
    
    void start(){
        gateway.start();
    }
    
    void sendInvoice(ClientOrderRequest request, ClientOrderReply reply){
        System.out.println("Sending reply from broker to client");
        gateway.sendReply(request, reply);
    }
}
