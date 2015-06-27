package com.marbl.warehouse.domain;

import java.io.Serializable;

public class Klant implements Serializable {

    /**
     * De code van de klant.
     */
    private int code;
    /**
     * De naam van de klant.
     */
    private String naam;
    /**
     * Het adres van de klant.
     */
    private String adres;

    /**
     * Er wordt een nieuw Klant-object aangemaakt met ingevoerde waardes.
     *
     * @param code De code van de klant.
     * @param naam De naam van de klant.
     * @param adres Het adres van de klant.
     */
    public Klant(int code, String naam, String adres) {
        this.code = code;
        this.naam = naam;
        this.adres = adres;
    }

    /**
     * Geeft de code van de klant.
     *
     * @return De code van de klant.
     */
    public int getCode() {
        return code;
    }

    /**
     * De naam van de klant.
     *
     * @return De naam van de klant.
     */
    public String getNaam() {
        return naam;
    }

    /**
     * Geeft het adres van de klant.
     *
     * @return Het adres van de klant.
     */
    public String getAdres() {
        return adres;
    }
}
