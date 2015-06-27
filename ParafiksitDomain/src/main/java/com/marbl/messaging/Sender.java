/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.messaging;

import java.util.logging.Level;
import java.util.logging.Logger;
import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.MessageProducer;
import javax.jms.Queue;
import javax.jms.TextMessage;
import javax.naming.NamingException;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
public class Sender extends Channel implements ISender{

private MessageProducer producer;    
    
    public Sender(String connectionName, String destinationName) throws NamingException, JMSException
    {
        super(connectionName, destinationName);  
        producer = session.createProducer(destination);
        //producer = session.createProducer((Queue)jndiContext.lookup(destinationName));
    }
      
    public TextMessage createMessage(String body) throws JMSException {
        return session.createTextMessage(body);
    }

    public boolean sendMessage(Message msg) {
        try {
            producer.send(msg);
            return true;
        } catch (JMSException ex) {
            Logger.getLogger(Sender.class.getName()).log(Level.SEVERE, null, ex);
            return false;
        }
    }

    public boolean sendMessage(Message msg, Destination dest) {
         try {
            //producer.send(destination, msg);
             producer.send(dest, msg);
            return true;
        } catch (JMSException ex) {
            Logger.getLogger(Sender.class.getName()).log(Level.SEVERE, null, ex);
            return false;
        }
    }    
}
