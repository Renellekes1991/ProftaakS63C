package com.marbl.warehouse.domain;

import java.io.Serializable;
import java.util.ArrayList;

public class Factuur implements Serializable {

    /**
     * De code van de factuur.
     */
    private int code;
    /**
     * Het code van de klant.
     */
    private int klantCode;
    /**
     * De datum van het factuur.
     */
    private String datum;
    /**
     * De lijst met FactuurRegel-objecten (bevat Onderdeel + aantal).
     */
    private ArrayList<FactuurRegel> regels;

    /**
     * Nieuw Factuur-object met ingevoerde waardes.
     *
     * @param code De code van de factuur.
     * @param klantCode De code van de klant.
     * @param datum De datum van de factuur.
     * @param regels Lijst met FactuurRegel objecten die bij de factuur
     * horen.
     */
    public Factuur(int code, int klantCode, String datum, ArrayList<FactuurRegel> regels) {
        this.code = code;
        this.klantCode = klantCode;
        this.datum = datum;
        if (regels != null) {
            this.regels = regels;
        } else {
            this.regels = new ArrayList<>();
        }
    }

    /**
     * Geeft de code van de factuur.
     *
     * @return De code van de factuur.
     */
    public int getCode() {
        return code;
    }

    /**
     * Geeft de code van de klant.
     *
     * @return De code van de klant.
     */
    public int getKlantCode() {
        return klantCode;
    }

    /**
     * Geeft de datum van de factuur.
     *
     * @return De datum van de factuur.
     */
    public String getDatum() {
        return datum;
    }

    /**
     * Geeft een lijst met FactuurRegel-objecten.
     *
     * @return Lijst met FactuurRegel-objecten.
     */
    public ArrayList<FactuurRegel> getRegels() {
        return regels;
    }

    public void setRegels(ArrayList<FactuurRegel> regels)
    {
        this.regels = regels;
    }
    
    
}
