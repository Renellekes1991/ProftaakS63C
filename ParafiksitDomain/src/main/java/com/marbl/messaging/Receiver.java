/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.messaging;

import java.util.logging.Level;
import java.util.logging.Logger;
import javax.jms.JMSException;
import javax.jms.MessageConsumer;
import javax.jms.MessageListener;
import javax.jms.Queue;
import javax.naming.NamingException;


public class Receiver extends Channel implements IReceiver{
    
    private MessageConsumer consumer;    
    
    public Receiver(String connectionName, String destinationName) throws NamingException, JMSException
    {        
        super(connectionName, destinationName); 
        consumer = session.createConsumer(destination);        
        //consumer = session.createConsumer((Queue)jndiContext.lookup(destinationName));
    }    

    public void setMessageListener(MessageListener listener) {
        try {
            consumer.setMessageListener(listener);
        } catch (JMSException ex) {
            Logger.getLogger(Receiver.class.getName()).log(Level.SEVERE, null, ex);
        }
    } 
}
