/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.computerbroker;

import com.marbl.client.ClientOrderReply;
import com.marbl.client.ClientOrderRequest;
import com.marbl.messaging.requestreply.IReplyListener;
import com.marbl.parafiksit.ParafiksitOrderReply;
import com.marbl.parafiksit.ParafiksitOrderRequest;
import com.marbl.warehouse.WarehouseOrderReply;
import com.marbl.warehouse.WarehouseOrderRequest;
import java.math.BigDecimal;
import java.util.ArrayList;


abstract class OrderRequestProcess
{

    private ClientOrderRequest clientRequest = null;
    private WarehouseOrderRequest warehouseRequest = null;
    private WarehouseOrderReply warehouseReply = null;
    private ParafiksitOrderRequest parafiksitRequest = null;
    private ParafiksitOrderReply parafiksitReply = null;
    private ClientOrderReply clientReply = null;
    private ClientOrderGateway clientGateway;
    private WarehouseOrderGateway warehouseGateway;
    private ParafiksitOrderGateway parafiksitGateway;
    private IReplyListener<WarehouseOrderRequest, WarehouseOrderReply> warehouseReplyListener;
    private IReplyListener<ParafiksitOrderRequest, ParafiksitOrderReply> parafiksitReplyListener;

    public OrderRequestProcess(ClientOrderRequest clientRequest, ClientOrderGateway clientGateway, WarehouseOrderGateway warehouseGateway, ParafiksitOrderGateway parafiksitGateway)
    {
        this.clientRequest = clientRequest;
        this.clientGateway = clientGateway;
        this.warehouseGateway = warehouseGateway;
        this.parafiksitGateway = parafiksitGateway;

        this.warehouseReplyListener = new IReplyListener<WarehouseOrderRequest, WarehouseOrderReply>()
        {
            public void onReply(WarehouseOrderRequest request, WarehouseOrderReply reply)
            {
                System.out.println("OrderRequestProcess got reply from warehouse");
                onWarehouseReply(reply);
            }
        };

        this.parafiksitReplyListener = new IReplyListener<ParafiksitOrderRequest, ParafiksitOrderReply>()
        {
            public void onReply(ParafiksitOrderRequest request, ParafiksitOrderReply reply)
            {
                System.out.println("OrderRequestProcess got reply from parafiksit");
                onParafiksitReply(reply);
            }
        };

        //CHECK OF ER ONDERDELEN ZITTEN IN DE ORDER!!!!!!
        //sendOrderToParafiksit();
        CheckIfOrderNeedsParts(clientRequest);
    }

    private void CheckIfOrderNeedsParts(ClientOrderRequest cRequest)
    {
        //zoja: stuur naar warehouse (-> parafiksit -> client)
        //zonee: stuur naar parafiksit (-> client)

        if (cRequest.containsParts())
        {
            orderPartsAtWarehouse(cRequest);
        } else
        {
            sendOrderToParafiksit(cRequest);
        }
    }

    /*
     * Warehouse methods
     */
    private void orderPartsAtWarehouse(ClientOrderRequest cRequest)
    {
        WarehouseOrderRequest whRequest = createWarehouseRequest(cRequest);
        warehouseGateway.orderParts(whRequest, warehouseReplyListener);
    }

    private WarehouseOrderRequest createWarehouseRequest(ClientOrderRequest cRequest)
    {
        //Check welke parts ordered moeten worden!
        return new WarehouseOrderRequest(cRequest.getContact(), cRequest.getShippingAddress(), (ArrayList) cRequest.getParts()); //geef parameters mee uit cRequest (onderdelen)
    }

    private void onWarehouseReply(WarehouseOrderReply whReply)
    {
        warehouseReply = whReply;
        notifyReceivedWarehouseReply(clientRequest, whReply);

        sendOrderToParafiksit(clientRequest);
    }

    abstract void notifyReceivedWarehouseReply(ClientOrderRequest clientRequest, WarehouseOrderReply warehouseReply);

    /*
     * Parafiksit
     */
    private void sendOrderToParafiksit(ClientOrderRequest cRequest)
    {
        ParafiksitOrderRequest req = createParafiksitRequest(cRequest);
        parafiksitGateway.registerOrder(req, parafiksitReplyListener);
    }

    private ParafiksitOrderRequest createParafiksitRequest(ClientOrderRequest cRequest)
    {
        System.out.println("cRequest.getstring: " + cRequest.getString());

        ParafiksitOrderRequest paraRequest;

        paraRequest = new ParafiksitOrderRequest((ArrayList) cRequest.getOperations(),cRequest.getContact(),cRequest.getShippingAddress());


        return paraRequest;
    }

    private void onParafiksitReply(ParafiksitOrderReply pReply)
    {
        parafiksitReply = pReply;
        notifyReceivedParafiksitReply(clientRequest, pReply);


        ClientOrderReply cReply = createClientReply(clientRequest, parafiksitReply);
        sendClientReply(cReply);
    }

    abstract void notifyReceivedParafiksitReply(ClientOrderRequest clientRequest, ParafiksitOrderReply reply);

    /*
     * ClientReply
     */
    private void sendClientReply(ClientOrderReply cReply)
    {
        clientGateway.sendInvoice(clientRequest, cReply);
        notifySentClientReply(this);
    }

    private ClientOrderReply createClientReply(ClientOrderRequest cRequest, ParafiksitOrderReply pReply)
    {
            //FACTUUR MAKEN HIER!!!
        
        //Prijs moet nog goed, is hardcoded!
        ClientOrderReply finalReply = new ClientOrderReply(cRequest.getClientName(), cRequest.getShippingAddress(), cRequest.getComments(), (ArrayList)cRequest.getOperations(), new BigDecimal(pReply.getTotalPriceForWorkPerformed()), (ArrayList)cRequest.getParts(), new BigDecimal(10), new BigDecimal(20), pReply.getBankAccount());
        return finalReply;
    }

    abstract void notifySentClientReply(OrderRequestProcess process);
}