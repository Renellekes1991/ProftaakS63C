/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.parafiksit;

import com.marbl.client.domain.Contact;
import com.marbl.client.domain.ShippingAddress;
import com.marbl.client.domain.WorkPerformedInfo;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class ParafiksitOrderRequest  implements Serializable
{
    private ShippingAddress shippingAddress;
    private Contact contact;
    private List<WorkPerformedInfo> workPerformed;

    public ParafiksitOrderRequest(ArrayList<WorkPerformedInfo> work, Contact contact, ShippingAddress shipping)
    {
        this.workPerformed = work;
        this.contact = contact;
        this.shippingAddress = shipping;
    }

    public Contact getContact()
    {
        return contact;
    }

    public ShippingAddress getShippingAddress()
    {
        return shippingAddress;
    }

    public List<WorkPerformedInfo> getWorkPerformed()
    {
        return workPerformed;
    }
    
    
    
    
    
}