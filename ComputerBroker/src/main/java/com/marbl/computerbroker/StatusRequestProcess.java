/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.computerbroker;

import com.marbl.client.ClientStatusReply;
import com.marbl.client.ClientStatusRequest;
import com.marbl.messaging.requestreply.IReplyListener;
import com.marbl.parafiksit.ParafiksitStatusReply;
import com.marbl.parafiksit.ParafiksitStatusRequest;
import com.marbl.warehouse.WarehouseStatusReply;
import com.marbl.warehouse.WarehouseStatusRequest;


abstract class StatusRequestProcess
{

    private ClientStatusRequest clientRequest = null;
    private ParafiksitStatusRequest parafiksitRequest = null;
    private ParafiksitStatusReply parafiksitReply = null;
    private ClientStatusReply clientReply = null;
    private ClientStatusGateway clientStatusGateway;
    private WarehouseStatusGateway warehouseStatusGateway;
    private ParafiksitStatusGateway parafiksitGateway;
    private IReplyListener<ParafiksitStatusRequest, ParafiksitStatusReply> parafiksitReplyListener;
    private IReplyListener<WarehouseStatusRequest, WarehouseStatusReply> warehouseReplyListener;

    public StatusRequestProcess(ClientStatusRequest clientRequest, ClientStatusGateway clientGateway, ParafiksitStatusGateway parafiksitGateway)
    {
        this.clientRequest = clientRequest;
        this.clientStatusGateway = clientGateway;
        this.parafiksitGateway = parafiksitGateway;

        this.parafiksitReplyListener = new IReplyListener<ParafiksitStatusRequest, ParafiksitStatusReply>()
        {
            public void onReply(ParafiksitStatusRequest request, ParafiksitStatusReply reply)
            {
                onParafiksitReply(reply);
            }
        };

        this.warehouseReplyListener = new IReplyListener<WarehouseStatusRequest, WarehouseStatusReply>()
        {
            public void onReply(WarehouseStatusRequest request, WarehouseStatusReply reply)
            {
                onWarehouseReply(reply);
            }
        };

        sendStatusRequestToWarehouse();

    }

    private void sendStatusRequestToWarehouse()
    {
        WarehouseStatusRequest wRequest = new WarehouseStatusRequest();
        warehouseStatusGateway.getStatus(wRequest, warehouseReplyListener);
    }

    private void onWarehouseReply(WarehouseStatusReply reply)
    {
        notifyReceivedWarehouseReply(clientRequest, reply);

        sendStatusRequestToParafiksit();
    }

    abstract void notifyReceivedWarehouseReply(ClientStatusRequest clientRequest, WarehouseStatusReply reply);

    private void sendStatusRequestToParafiksit()
    {
        ParafiksitStatusRequest pRequest = new ParafiksitStatusRequest();
        parafiksitGateway.getStatus(pRequest, parafiksitReplyListener);
    }

    private void onParafiksitReply(ParafiksitStatusReply reply)
    {
        notifyReceivedParafiksitReply(clientRequest, reply);

        ClientStatusReply cReply = new ClientStatusReply();
        clientStatusGateway.sendReply(clientRequest, cReply);

        notifySentClientReply(this);
    }

    abstract void notifyReceivedParafiksitReply(ClientStatusRequest clientRequest, ParafiksitStatusReply reply);

    abstract void notifySentClientReply(StatusRequestProcess process);
}
