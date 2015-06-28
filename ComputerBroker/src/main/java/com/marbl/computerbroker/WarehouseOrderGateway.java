/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.computerbroker;

import com.marbl.messaging.IReceiver;
import com.marbl.messaging.requestreply.AsynchronousRequestor;
import com.marbl.messaging.requestreply.IReplyListener;
import com.marbl.warehouse.WarehouseOrderReply;
import com.marbl.warehouse.WarehouseOrderRequest;
import com.marbl.warehouse.WarehouseSerializer;
import java.util.Hashtable;
import java.util.logging.Level;
import java.util.logging.Logger;


public class WarehouseOrderGateway {
    
    private WarehouseSerializer serializer;
    private AsynchronousRequestor<WarehouseOrderRequest, WarehouseOrderReply> gateway;
    
    public WarehouseOrderGateway(String factoryName, String warehouseRequestQueue, String warehouseReplyQueue)
    {
        serializer = new WarehouseSerializer();
        try {
            gateway = new AsynchronousRequestor<WarehouseOrderRequest, WarehouseOrderReply>(factoryName, warehouseRequestQueue, warehouseReplyQueue, serializer);
        } catch (Exception ex) {
            Logger.getLogger(WarehouseOrderGateway.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    public void start(){
        gateway.start();
    }
    
    public void orderParts(WarehouseOrderRequest request, IReplyListener<WarehouseOrderRequest, WarehouseOrderReply> listener){
        System.out.println("Sending request from broker to warehouse");
        gateway.sendRequest(request, listener);
    }
    
}
