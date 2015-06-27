package com.marbl.parafiksit;

import com.thoughtworks.xstream.XStream;
import com.marbl.messaging.requestreply.IRequestReplySerializer;

/**
 *
 * This class serializes BankRequest and Bankreply to/from XML.
 */
public class ParafiksitStatusSerializer implements IRequestReplySerializer<ParafiksitStatusRequest, ParafiksitStatusReply> {

    private static final String ALIAS_REQUEST = "ParafiksitStatusRequest";
    private static final String ALIAS_REPLY = "ParafiksitStatusReply"; 
    private XStream xstream; // class for serialization

    public ParafiksitStatusSerializer() {
        super();
        xstream = new XStream();
         // register aliases (i.e., tag names)
        xstream.alias(ALIAS_REQUEST, ParafiksitStatusRequest.class);
        xstream.alias(ALIAS_REPLY, ParafiksitStatusReply.class);
    }

    /**
     * This method parses a bankRequest from an XML string.
     * @param str is the string containing the XML
     * @return the BankRequest containng the same information like the given XML (str)
     */
    public ParafiksitStatusRequest requestFromString(String str) {
        return (ParafiksitStatusRequest) xstream.fromXML(str);
    }
    /**
     * This method parses a BankReply from an XML string.
     * @param str is the string containing the XML
     * @return the BankReply containng the same information like the given XML (str)
     */
    public ParafiksitStatusReply replyFromString(String str) {
        return (ParafiksitStatusReply) xstream.fromXML(str);
    }
    
    /**
     * Serializes a BankRequest into an XML string.
     * @param request is the BankRequest to be serialized into XML
     * @return the string containing XML with information about the request
     */
    public String requestToString(ParafiksitStatusRequest request) {
        return xstream.toXML(request);
    }
    /**
     * Serializes a BankReply into XML string.
     * @param request is the BankReply to be serialized into XML
     * @return the string containing XML with information about the reply
     */
    public String replyToString(ParafiksitStatusReply reply) {
        return xstream.toXML(reply);
    }
}
