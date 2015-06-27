package com.marbl.client.domain;

import java.io.Serializable;

public class ShippingAddress  implements Serializable{

    private String street;
    private String number;
    private String postalCode;
    private String place;

    public ShippingAddress(String street, String number, String postalCode, String place) {
        this.street = street;
        this.number = number;
        this.postalCode = postalCode;
        this.place = place;
    }

    public String getStreet() {
        return street;
    }
    
    public void setStreet(String street) {
        this.street = street;
    }

    public String getNumber() {
        return number;
    }
    
    public void setNumber(String number) {
        this.number = number;
    }

    public String getPostalCode() {
        return postalCode;
    }
    
    public void setPostalCode(String postalCode) {
        this.postalCode = postalCode;
    }

    public String getPlace() {
        return place;
    }
    
    public void setPlace(String place) {
        this.place = place;
    }
}
