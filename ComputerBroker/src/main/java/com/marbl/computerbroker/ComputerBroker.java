/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.computerbroker;

import com.marbl.client.ClientOrderRequest;
import com.marbl.client.ClientStatusRequest;
import com.marbl.parafiksit.ParafiksitOrderReply;
import com.marbl.parafiksit.ParafiksitStatusReply;
import com.marbl.warehouse.WarehouseOrderReply;
import com.marbl.warehouse.WarehouseStatusReply;
import java.util.ArrayList;


public class ComputerBroker {
    
    private ClientOrderGateway clientOrderGateway;
    private ParafiksitOrderGateway parafiksitOrderGateway;
    private WarehouseOrderGateway warehouseOrderGateway;
    
    private ClientStatusGateway clientStatusGateway;
    private ParafiksitStatusGateway parafiksitStatusGateway;
    
    private ArrayList<OrderRequestProcess> activeClientOrderProcesses;
    private ArrayList<StatusRequestProcess> activeClientStatusProcesses;
    
    public ComputerBroker(String factoryName, String clientOrderRequestQueue, String parafiksitOrderRequestQueue, 
            String parafiksitOrderReplyQueue, String warehouseRequestQueue, String warehouseReplyQueue, 
            String clientStatusRequestQueue, String parafiksitStatusRequestQueue, String parafiksitStatusReplyQueue)
    {
        super();
        
        activeClientOrderProcesses = new ArrayList<OrderRequestProcess>();
        activeClientStatusProcesses = new ArrayList<StatusRequestProcess>();
                
        clientOrderGateway = new ClientOrderGateway(factoryName, clientOrderRequestQueue) {
            @Override
            void onClientRequest(ClientOrderRequest request) {
                ComputerBroker.this.onClientOrderRequest(request);
            }
        };
        parafiksitOrderGateway = new ParafiksitOrderGateway(factoryName, parafiksitOrderRequestQueue, parafiksitOrderReplyQueue) {};
        warehouseOrderGateway = new WarehouseOrderGateway(factoryName, warehouseRequestQueue, warehouseReplyQueue);
    
        clientStatusGateway = new ClientStatusGateway(factoryName, clientStatusRequestQueue){
            @Override
            void onClientRequest(ClientStatusRequest request) {
                ComputerBroker.this.onClientStatusRequest(request);
            }    
        };
        parafiksitStatusGateway = new ParafiksitStatusGateway(factoryName, parafiksitStatusRequestQueue, parafiksitStatusReplyQueue){};
    }
    
    private void onClientOrderRequest(ClientOrderRequest request)
    {
        final OrderRequestProcess p = new OrderRequestProcess(request, clientOrderGateway, warehouseOrderGateway, parafiksitOrderGateway){
            
            
            @Override
            void notifyReceivedParafiksitReply(ClientOrderRequest clientRequest, ParafiksitOrderReply reply){
                //?
            }
            
            @Override
            void notifyReceivedWarehouseReply(ClientOrderRequest clientRequest, WarehouseOrderReply reply){
                //?
            }

            @Override
            void notifySentClientReply(OrderRequestProcess process) {
                activeClientOrderProcesses.remove(process);
            }
        };
        activeClientOrderProcesses.add(p);   
    }
    
    private void onClientStatusRequest(ClientStatusRequest request){
        final StatusRequestProcess p = new StatusRequestProcess(request, clientStatusGateway, parafiksitStatusGateway) {

            @Override
            void notifyReceivedParafiksitReply(ClientStatusRequest clientRequest, ParafiksitStatusReply reply) {
                //
            }

            @Override
            void notifySentClientReply(StatusRequestProcess process) {
                activeClientStatusProcesses.remove(process);
            }

            @Override
            void notifyReceivedWarehouseReply(ClientStatusRequest clientRequest, WarehouseStatusReply reply)
            {
             //
            }
        };
        activeClientStatusProcesses.add(p);
    }
    
    public void start(){
        clientOrderGateway.start();
        parafiksitOrderGateway.start();
        warehouseOrderGateway.start();
        
        clientStatusGateway.start();
        parafiksitStatusGateway.start();
    }  
}
