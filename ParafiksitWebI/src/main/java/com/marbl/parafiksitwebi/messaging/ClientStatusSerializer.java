package com.marbl.parafiksitwebi.messaging;

import com.marbl.client.ClientStatusReply;
import com.marbl.client.ClientStatusRequest;
import com.marbl.messaging.requestreply.IRequestReplySerializer;
import com.thoughtworks.xstream.XStream;

/**
 * This class serializes ClientOrderReply and ClientOrderRequest to and from XML.
 */
public class ClientStatusSerializer implements IRequestReplySerializer<ClientStatusRequest, ClientStatusReply> {

    private static final String ALIAS_REQUEST = "ClientStatusRequest"; // the tag name for ClientOrderRequest
    private static final String ALIAS_REPLY = "ClientStatusReply"; // the tag name for ClientOrderReply
    private XStream xstream; // for easy XML serialization

    public ClientStatusSerializer() {
        super();
        xstream = new XStream();
         // register aliases (tag names)
        xstream.alias(ALIAS_REQUEST, ClientStatusRequest.class);
        xstream.alias(ALIAS_REPLY, ClientStatusReply.class);
    }
    
    /**
     * This method parses a ClientOrderRequest from an XML string.
     * @param str is the string containing the XML
     * @return the ClientOrderRequest containng the same information like the given XML (str)
     */
    public ClientStatusRequest requestFromString(String str) {
        return (ClientStatusRequest) xstream.fromXML(str);
    }

    /**
     * This method parses a ClientOrderReply from an XML string.
     * @param str is the string containing the XML
     * @return the ClientOrderReply containng the same information like the given XML (str)
     */
    public ClientStatusReply replyFromString(String str) {
        return (ClientStatusReply) xstream.fromXML(str);
    }

    /**
     * Serializes a ClientOrderRequest into XML string.
     * @param request is the ClientOrderRequest to be serialized into XML
     * @return the string containing XML with information about the request
     */
    public String requestToString(ClientStatusRequest request) {
        return xstream.toXML(request);
    }
    
    /**
     * Serializes a ClientOrderReply into XML string.
     * @param reply is the ClientOrderReply to be serialized into XML
     * @return the string containing XML with information about the rereply
     */
    public String replyToString(ClientStatusReply reply) {
        return xstream.toXML(reply);
    }
}
