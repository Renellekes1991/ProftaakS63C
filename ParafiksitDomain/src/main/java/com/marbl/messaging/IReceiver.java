/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.messaging;

import javax.jms.MessageListener;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
public interface IReceiver extends IChannel {
    void setMessageListener(MessageListener listener);
}
