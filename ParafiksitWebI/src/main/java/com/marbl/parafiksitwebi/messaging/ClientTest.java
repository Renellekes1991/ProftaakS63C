/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.parafiksitwebi.messaging;

import com.marbl.client.ClientOrderReply;
import com.marbl.client.ClientOrderRequest;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
public class ClientTest {
    
    private String clientName;
    private ComputerBrokerOrderGateway orderGateway;
    
    public ClientTest(String clientName, String factoryName, String requestQueue, String replyQueue){
        this.clientName = clientName;
        orderGateway = new ComputerBrokerOrderGateway(factoryName, requestQueue, replyQueue) {

            @Override
            void onOrderReplyArrived(ClientOrderRequest request, ClientOrderReply reply) {
            }
        };
    }
    
    public void start(){
        orderGateway.start();
    }
    
    public void sendOrderRequest(ClientOrderRequest request){
        orderGateway.order(request);
    }
    
    public String getClientName(){
        return clientName;
    }
}
