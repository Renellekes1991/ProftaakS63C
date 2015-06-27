package com.marbl.client.domain;

import java.io.Serializable;

public class Contact implements Serializable {

    private String firstName;
    private String lastName;
    private String contactPhone;

    public Contact(String firstName, String lastName, String contactPhone) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.contactPhone = contactPhone;
    }

    public String getFirstName() {
        return firstName;
    }
    
    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }
    
    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getContactPhone() {
        return contactPhone;
    }

    public void setContactPhone(String contactPhone) {
        this.contactPhone = contactPhone;
    }

    public String getContactName() {
        return firstName + " " + lastName;
    }
}
