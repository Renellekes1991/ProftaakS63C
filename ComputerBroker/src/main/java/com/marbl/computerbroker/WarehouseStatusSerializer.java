/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.computerbroker;

import com.marbl.client.ClientOrderReply;
import com.marbl.client.ClientOrderRequest;
import com.marbl.client.ClientStatusReply;
import com.marbl.client.ClientStatusRequest;
import com.marbl.messaging.requestreply.IRequestReplySerializer;
import com.marbl.warehouse.WarehouseStatusReply;
import com.marbl.warehouse.WarehouseStatusRequest;
import com.thoughtworks.xstream.XStream;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
class WarehouseStatusSerializer implements IRequestReplySerializer<WarehouseStatusRequest, WarehouseStatusReply> 
{

    private static final String ALIAS_REQUEST = "WarehouseStatusRequest"; // the tag name for WarehouseStatusRequest
    private static final String ALIAS_REPLY = "WarehouseStatusReply"; // the tag name for WarehouseStatusReply
    private XStream xstream; // for easy XML serialization

    public WarehouseStatusSerializer() {
        super();
        xstream = new XStream();
         // register aliases (tag names)
        xstream.alias(ALIAS_REQUEST, WarehouseStatusRequest.class);
        xstream.alias(ALIAS_REPLY, WarehouseStatusReply.class);
    }
    
    /**
     * This method parses a ClientOrderRequest from an XML string.
     * @param str is the string containing the XML
     * @return the ClientOrderRequest containng the same information like the given XML (str)
     */
    public WarehouseStatusRequest requestFromString(String str) {
        return (WarehouseStatusRequest) xstream.fromXML(str);
    }

    /**
     * This method parses a ClientOrderReply from an XML string.
     * @param str is the string containing the XML
     * @return the ClientOrderReply containng the same information like the given XML (str)
     */
    public WarehouseStatusReply replyFromString(String str) {
        return (WarehouseStatusReply) xstream.fromXML(str);
    }

    /**
     * Serializes a ClientOrderRequest into XML string.
     * @param request is the ClientOrderRequest to be serialized into XML
     * @return the string containing XML with information about the request
     */
    public String requestToString(WarehouseStatusRequest request) {
        return xstream.toXML(request);
    }
    
    /**
     * Serializes a ClientOrderReply into XML string.
     * @param reply is the ClientOrderReply to be serialized into XML
     * @return the string containing XML with information about the rereply
     */
    public String replyToString(WarehouseStatusReply reply) {
        return xstream.toXML(reply);
    }   
}
