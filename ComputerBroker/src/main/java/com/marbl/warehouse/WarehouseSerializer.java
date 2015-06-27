package com.marbl.warehouse;

import com.thoughtworks.xstream.XStream;
import com.marbl.messaging.requestreply.IRequestReplySerializer;

/**
 *
 * This class serializes BankRequest and Bankreply to/from XML.
 */
public class WarehouseSerializer implements IRequestReplySerializer<WarehouseOrderRequest, WarehouseOrderReply> {

    private static final String ALIAS_REQUEST = "WarehouseRequest"; // tag name for BankRequest
    private static final String ALIAS_REPLY = "WarehouseReply"; // tag name for BankReply
    private XStream xstream; // class for serialization

    public WarehouseSerializer() {
        super();
        xstream = new XStream();
         // register aliases (i.e., tag names)
        xstream.alias(ALIAS_REQUEST, WarehouseOrderRequest.class);
        xstream.alias(ALIAS_REPLY, WarehouseOrderReply.class);
    }

    /**
     * This method parses a bankRequest from an XML string.
     * @param str is the string containing the XML
     * @return the BankRequest containng the same information like the given XML (str)
     */
    public WarehouseOrderRequest requestFromString(String str) {
        return (WarehouseOrderRequest) xstream.fromXML(str);
    }
    /**
     * This method parses a BankReply from an XML string.
     * @param str is the string containing the XML
     * @return the BankReply containng the same information like the given XML (str)
     */
    public WarehouseOrderReply replyFromString(String str) {
        return (WarehouseOrderReply) xstream.fromXML(str);
    }
    
    /**
     * Serializes a BankRequest into an XML string.
     * @param request is the BankRequest to be serialized into XML
     * @return the string containing XML with information about the request
     */
    public String requestToString(WarehouseOrderRequest request) {
        return xstream.toXML(request);
    }
    /**
     * Serializes a BankReply into XML string.
     * @param request is the BankReply to be serialized into XML
     * @return the string containing XML with information about the reply
     */
    public String replyToString(WarehouseOrderReply reply) {
        return xstream.toXML(reply);
    }
}
