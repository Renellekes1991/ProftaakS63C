package com.marbl.client;

import com.marbl.messaging.requestreply.IRequestReplySerializer;
import com.thoughtworks.xstream.XStream;

/**
 * This class serializes ClientOrderReply and ClientOrderRequest to and from XML.
 */
public class ClientOrderSerializer implements IRequestReplySerializer<ClientOrderRequest, ClientOrderReply> {

    private static final String ALIAS_REQUEST = "ClientOrderRequest"; // the tag name for ClientOrderRequest
    private static final String ALIAS_REPLY = "ClientOrderReply"; // the tag name for ClientOrderReply
    private XStream xstream; // for easy XML serialization

    public ClientOrderSerializer() {
        super();
        xstream = new XStream();
         // register aliases (tag names)
        xstream.alias(ALIAS_REQUEST, ClientOrderRequest.class);
        xstream.alias(ALIAS_REPLY, ClientOrderReply.class);
    }
    
    /**
     * This method parses a ClientOrderRequest from an XML string.
     * @param str is the string containing the XML
     * @return the ClientOrderRequest containng the same information like the given XML (str)
     */
    public ClientOrderRequest requestFromString(String str) {
        return (ClientOrderRequest) xstream.fromXML(str);
    }

    /**
     * This method parses a ClientOrderReply from an XML string.
     * @param str is the string containing the XML
     * @return the ClientOrderReply containng the same information like the given XML (str)
     */
    public ClientOrderReply replyFromString(String str) {
        return (ClientOrderReply) xstream.fromXML(str);
    }

    /**
     * Serializes a ClientOrderRequest into XML string.
     * @param request is the ClientOrderRequest to be serialized into XML
     * @return the string containing XML with information about the request
     */
    public String requestToString(ClientOrderRequest request) {
        return xstream.toXML(request);
    }
    
    /**
     * Serializes a ClientOrderReply into XML string.
     * @param reply is the ClientOrderReply to be serialized into XML
     * @return the string containing XML with information about the rereply
     */
    public String replyToString(ClientOrderReply reply) {
        return xstream.toXML(reply);
    }
}
