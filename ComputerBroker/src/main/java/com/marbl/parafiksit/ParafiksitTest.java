/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.parafiksit;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
public class ParafiksitTest {
    
    private ComputerBrokerOrderGateway orderGateway;
    
    public ParafiksitTest(String factoryName, String requestQueue, String replyQueue){
        orderGateway = new ComputerBrokerOrderGateway(factoryName, requestQueue, replyQueue) {

            @Override
            void receivedParafiksitRequest(ParafiksitOrderRequest request) {
                
                //DO STUFF HERE
              //  orderGateway.sendReply(request, new ParafiksitOrderReply(null));
            }
        };
    }
    
    public void start(){
        orderGateway.start();
    }
}
