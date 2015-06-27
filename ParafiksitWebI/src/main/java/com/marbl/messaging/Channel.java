/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.messaging;

import java.util.Properties;
import javax.jms.Connection;
import javax.jms.ConnectionFactory;
import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.Session;
import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
public class Channel implements IChannel {

    protected Connection connection;
    protected Session session;
    protected Destination destination;
    
    Context jndiContext;
    
    public Channel(String connectionName, String destinationName) throws NamingException, JMSException {
        
        //Properties properties = new Properties();
        //properties.setProperty("java.naming.factory.initial", "com.sun.enterprise.naming.SerialInitContextFactory");
        //properties.setProperty("java.naming.factory.url.pkgs", "com.sun.enterprise.naming");
        //properties.setProperty("java.naming.factory.state", "com.sun.corba.ee.impl.presentation.rmi.JNDIStateFactoryImpl");
        //properties.setProperty("org.omg.CORBA.ORBInitialHost", "145.93.48.239");
        //properties.setProperty("org.omg.CORBA.ORBInitialPort", "7676"); //3700
        
        //jndiContext = new InitialContext(properties);
        jndiContext = new InitialContext();

        ConnectionFactory connectionFactory = (ConnectionFactory) jndiContext.lookup(connectionName);
        connection = connectionFactory.createConnection();
        session = connection.createSession(false, Session.AUTO_ACKNOWLEDGE);

        //if (destinationName.equals("") || 
        if(destinationName == null) {
            destination = null;
        } else {
            destination = (Destination) jndiContext.lookup(destinationName);
        }
    }

    public Destination getDestination() {
        return destination;
    }

    public void openConnection() throws JMSException {

        connection.start();
    }  
}
