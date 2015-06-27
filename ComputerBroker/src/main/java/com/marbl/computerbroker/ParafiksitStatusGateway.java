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
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
abstract class ParafiksitStatusGateway
{

    private ParafiksitStatusSerializer serializer;
    private AMQAsynchronousRequestor<ParafiksitStatusRequest, ParafiksitStatusReply> gateway;

    public ParafiksitStatusGateway(String factoryName, String parafiksitRequestQueue, String parafiksitReplyQueue)
    {
        serializer = new ParafiksitStatusSerializer();
        try
        {
            gateway = new AMQAsynchronousRequestor<ParafiksitStatusRequest, ParafiksitStatusReply>(factoryName, parafiksitRequestQueue, parafiksitReplyQueue, serializer);
        } catch (Exception ex)
        {
            Logger.getLogger(ParafiksitStatusGateway.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public void start()
    {
        gateway.start();
    }

    public void getStatus(ParafiksitStatusRequest request, IReplyListener<ParafiksitStatusRequest, ParafiksitStatusReply> listener)
    {
        gateway.sendRequest(request, listener);
    }
}
