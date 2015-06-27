package com.marbl.client;

import com.marbl.client.domain.Contact;
import com.marbl.client.domain.PartInfo;
import com.marbl.client.domain.ShippingAddress;
import com.marbl.client.domain.WorkPerformedInfo;
import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

public class ClientOrderRequest implements Serializable{

    private String clientName;
    private Contact contact;
    private ShippingAddress shippingAddress;
    private String comments;
    private List<WorkPerformedInfo> operations;
    private List<PartInfo> parts;
    
    public ClientOrderRequest() {
        this("", "", "", "", "", "", "", "", new ArrayList<WorkPerformedInfo>(), new ArrayList<PartInfo>());
    }

    public ClientOrderRequest(String clientName, String firstName, String lastName, String contactPhone, String shippingStreet, String shippingNumber, String shippingPlace, String shippingPostalCode, ArrayList<WorkPerformedInfo> operations, ArrayList<PartInfo> parts) {
        this.clientName = clientName;
        this.contact = new Contact(firstName, lastName, contactPhone);
        this.shippingAddress = new ShippingAddress(shippingStreet, shippingNumber, shippingPostalCode, shippingPlace);
        this.operations = operations;
        this.parts = parts;
    }

    public String getClientName() {
        return clientName;
    }

    public void setClientName(String clientName) {
        this.clientName = clientName;
    }

    public Contact getContact() {
        return contact;
    }

    public void setContact(Contact contact) {
        this.contact = contact;
    }

    public String getComments() {
        return comments;
    }

    public void setComments(String comments) {
        this.comments = comments;
    }

    public void setShippingAddress(ShippingAddress shippingAddress) {
        this.shippingAddress = shippingAddress;
    }

    public void setParts(ArrayList<PartInfo> parts) {
        this.parts = parts;
    }

    public void setOperations(ArrayList<WorkPerformedInfo> operations) {
        this.operations = operations;
    }

    public ShippingAddress getShippingAddress() {
        return shippingAddress;
    }

    public List<PartInfo> getParts() {
        return parts;
    }

    public List<WorkPerformedInfo> getOperations() {
        return operations;
    }

    public String getString() {
        return clientName + " " + comments;
    }

    public boolean containsParts() {
        if (parts.size() > 0) {
            return true;

        } else {
            return false;
        }
    }
}
