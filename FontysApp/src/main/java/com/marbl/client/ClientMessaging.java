/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.client;

import com.marbl.fontysapp.FactuurFrame;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
public class ClientMessaging {
    
    private String clientName;
    private ComputerBrokerOrderGateway orderGateway;
    
    public ClientMessaging(String clientName, String factoryName, String requestQueue, String replyQueue){
        this.clientName = clientName;
        orderGateway = new ComputerBrokerOrderGateway(factoryName, requestQueue, replyQueue) {

            @Override
            void onOrderReplyArrived(ClientOrderRequest request, ClientOrderReply reply) {
                
                //HIER MOET FACTUUR WORDEN GETOOND
                FactuurFrame fframe = new FactuurFrame(reply);
                fframe.setVisible(true);
                
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
