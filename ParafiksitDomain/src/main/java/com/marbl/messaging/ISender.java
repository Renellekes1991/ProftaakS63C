/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.messaging;

import javax.jms.Destination;
import javax.jms.JMSException;
import javax.jms.Message;
import javax.jms.TextMessage;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
public interface ISender extends IChannel{
    TextMessage createMessage(String body) throws JMSException;
    
    boolean sendMessage(Message msg) throws JMSException;
    
    boolean sendMessage(Message msg, Destination dest) throws JMSException;
}
