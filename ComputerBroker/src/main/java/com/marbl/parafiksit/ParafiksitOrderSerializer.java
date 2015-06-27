package com.marbl.parafiksit;

import com.marbl.client.domain.WorkPerformedInfo;
import com.thoughtworks.xstream.XStream;
import com.marbl.messaging.requestreply.IRequestReplySerializer;

/**
 *
 * This class serializes BankRequest and Bankreply to/from XML.
 */
public class ParafiksitOrderSerializer implements IRequestReplySerializer<ParafiksitOrderRequest, ParafiksitOrderReply> {

    private static final String ALIAS_REQUEST = "ParafiksitOrderRequest"; // tag name for BankRequest
    private static final String ALIAS_REPLY = "ParafiksitOrderReply"; // tag name for BankReply
     private static final String ALIAS_WORKPERFORMED = "WorkPerformedInfo"; // tag name for BankReply
    private XStream xstream; // class for serialization

    public ParafiksitOrderSerializer() {
        super();
        xstream = new XStream();
         // register aliases (i.e., tag names)
        xstream.alias(ALIAS_REQUEST, ParafiksitOrderRequest.class);
        xstream.alias(ALIAS_REPLY, ParafiksitOrderReply.class);
        xstream.alias(ALIAS_WORKPERFORMED, WorkPerformedInfo.class);
    }

    /**
     * This method parses a bankRequest from an XML string.
     * @param str is the string containing the XML
     * @return the BankRequest containng the same information like the given XML (str)
     */
    public ParafiksitOrderRequest requestFromString(String str) {
        return (ParafiksitOrderRequest) xstream.fromXML(str);
    }
    /**
     * This method parses a BankReply from an XML string.
     * @param str is the string containing the XML
     * @return the BankReply containng the same information like the given XML (str)
     */
    public ParafiksitOrderReply replyFromString(String str) {
        return (ParafiksitOrderReply) xstream.fromXML(str);
    }
    
    /**
     * Serializes a BankRequest into an XML string.
     * @param request is the BankRequest to be serialized into XML
     * @return the string containing XML with information about the request
     */
    public String requestToString(ParafiksitOrderRequest request) {
        return xstream.toXML(request);
    }
    /**
     * Serializes a BankReply into XML string.
     * @param request is the BankReply to be serialized into XML
     * @return the string containing XML with information about the reply
     */
    public String replyToString(ParafiksitOrderReply reply) {
        return xstream.toXML(reply);
    }
}
