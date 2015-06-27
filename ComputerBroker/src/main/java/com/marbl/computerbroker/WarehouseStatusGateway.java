/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.computerbroker;

import com.marbl.messaging.requestreply.AMQAsynchronousRequestor;
import com.marbl.messaging.requestreply.AsynchronousRequestor;
import com.marbl.messaging.requestreply.IReplyListener;
import com.marbl.parafiksit.ParafiksitStatusReply;
import com.marbl.parafiksit.ParafiksitStatusRequest;
import com.marbl.parafiksit.ParafiksitStatusSerializer;
import com.marbl.warehouse.WarehouseSerializer;
import com.marbl.warehouse.WarehouseStatusReply;
import com.marbl.warehouse.WarehouseStatusRequest;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
class WarehouseStatusGateway
{
    private WarehouseStatusSerializer serializer;
    private AsynchronousRequestor<WarehouseStatusRequest, WarehouseStatusReply> gateway;

    public WarehouseStatusGateway(String factoryName, String warehouseStatusRequestQueue, String warehouseStatusReplyQueue)
    {
        serializer = new WarehouseStatusSerializer();
        try
        {
            gateway = new AsynchronousRequestor<WarehouseStatusRequest, WarehouseStatusReply>(factoryName, warehouseStatusRequestQueue, warehouseStatusReplyQueue, serializer);
        } catch (Exception ex)
        {
            Logger.getLogger(ParafiksitStatusGateway.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public void start()
    {
        gateway.start();
    }

    public void getStatus(WarehouseStatusRequest request, IReplyListener<WarehouseStatusRequest, WarehouseStatusReply> listener)
    {
        gateway.sendRequest(request, listener);
    }
}
