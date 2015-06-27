/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.warehouse;

import com.marbl.client.domain.Contact;
import com.marbl.client.domain.PartInfo;
import com.marbl.client.domain.ShippingAddress;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
public class WarehouseOrderRequest  implements Serializable{
    
    private Contact contact;
    private ShippingAddress shipping;
    private List<PartInfo> parts;

    public WarehouseOrderRequest(Contact contact, ShippingAddress shipping, ArrayList<PartInfo> parts)
    {
        this.contact = contact;
        this.parts = parts;
        this.shipping = shipping;
    }

    public List<PartInfo> getParts()
    {
        return parts;
    }

    public void setParts(List<PartInfo> parts)
    {
        this.parts = parts;
    }

    public Contact getContact()
    {
        return contact;
    }

    public void setContact(Contact contact)
    {
        this.contact = contact;
    }

    public void setShipping(ShippingAddress shipping)
    {
        this.shipping = shipping;
    }

    public ShippingAddress getShipping()
    {
        return shipping;
    }    
    
}
