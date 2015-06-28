/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.warehouse;

import com.marbl.messaging.requestreply.AsynchronousReplier;
import com.marbl.messaging.requestreply.IRequestListener;
import java.util.logging.Level;
import javax.jms.Message;


abstract class ComputerBrokerGateway {

    private WarehouseSerializer serializer;
    private AsynchronousReplier<WarehouseOrderRequest, WarehouseOrderReply> gateway;

    public ComputerBrokerGateway(String factoryName, String warehouseRequestQueue, String warehouseReplyQueue) {
        serializer = new WarehouseSerializer();

        try {
            gateway = new AsynchronousReplier<WarehouseOrderRequest, WarehouseOrderReply>(factoryName, warehouseRequestQueue, serializer) {
                @Override
                public void beforeSendReply(Message request, Message reply) {
                    //???????
                }
            };
        } catch (Exception ex) {
            java.util.logging.Logger.getLogger(ComputerBrokerGateway.class.getName()).log(Level.SEVERE, null, ex);
        }
        
        gateway.setRequestListener(new IRequestListener<WarehouseOrderRequest>() {

            public void receivedRequest(WarehouseOrderRequest request) {
                receivedWarehouseRequest(request);
            }
        });
    }
    
    abstract void receivedWarehouseRequest(WarehouseOrderRequest request);
    
    public void start()
    {
        gateway.start();
    }
    
    void sendReply(WarehouseOrderRequest request, WarehouseOrderReply reply)
    {
        gateway.sendReply(request, reply);
    }
    
}
