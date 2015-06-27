/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.messaging;

import java.util.logging.Level;
import java.util.logging.Logger;
import javax.jms.Connection;
import javax.jms.DeliveryMode;
import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.MessageConsumer;
import javax.jms.MessageListener;
import javax.jms.MessageProducer;
import javax.jms.Session;
import javax.jms.TextMessage;
import org.apache.activemq.ActiveMQConnection;
import org.apache.activemq.ActiveMQConnectionFactory;

/**
 *
 * @author Stertefeld
 */
public class AMQGateway {
    
    private String user = ActiveMQConnection.DEFAULT_USER;
    private String password = ActiveMQConnection.DEFAULT_PASSWORD;
    private String url = ActiveMQConnection.DEFAULT_BROKER_URL;
    private Destination requestDestination;
    private Destination replyDestination;
    private Session session;
    private MessageProducer producer;
    private MessageConsumer consumer;
    private Connection connection;
    
    public AMQGateway() throws JMSException
    {
        connection = null;
        ActiveMQConnectionFactory connectionFactory = new ActiveMQConnectionFactory(user, password, url);
        connection = connectionFactory.createConnection();
        openConnection(); 
        
        requestDestination = session.createQueue("parafiksitOrderRequestQueue");
        producer = session.createProducer(requestDestination);
        producer.setDeliveryMode(DeliveryMode.NON_PERSISTENT);
        
        replyDestination = session.createQueue("parafiksitOrderReplyQueue");
        consumer = session.createConsumer(replyDestination);
    }
    
    public boolean sendMessage(Message message)
    {
        try
        {
            producer.send(message);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public boolean sendMessage(Destination destination, Message message)
    {
        try
        {
            producer.send(destination, message);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    public TextMessage createMessage(String body)
    {
       try
       {
           TextMessage message = session.createTextMessage(body);
           return message;
       } catch (JMSException ex) {
           Logger.getLogger(MessagingGateway.class.getName()).log(Level.SEVERE, null, ex);
       }
        
        return null;
    }
    
    public void setReceivedMessageListener(MessageListener listener) throws JMSException
    {
        consumer.setMessageListener(listener);
    }
    
    public void openConnection()
    {
        try {
            connection.start();
            session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);
        } catch (JMSException ex) {
            Logger.getLogger(AMQGateway.class.getName()).log(Level.SEVERE, null, ex);
        }
    }
    
    public Destination getReceiverDestination()
    {
        return requestDestination;
    }
    
    public Destination getSenderDesination()
    {
        return replyDestination;
    }
}
