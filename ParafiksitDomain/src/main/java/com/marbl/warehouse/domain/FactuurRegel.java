package com.marbl.warehouse.domain;

import java.io.Serializable;

public class FactuurRegel implements Serializable {

    /**
     * De code van de factuur.
     */
    private int factuurCode;
    /**
     * De code van het onderdeel.
     */
    private int onderdeelCode;
    /**
     * Het aantal van het onderdeel.
     */
    private int aantal;

    /**
     * Een nieuw FactuurRegel object met ingevoerde waardes.
     *
     * @param factuurCode
     * @param onderdeelCode
     * @param aantal
     */
    public FactuurRegel(int factuurCode, int onderdeelCode, int aantal) {
        this.factuurCode = factuurCode;
        this.onderdeelCode = onderdeelCode;
        this.aantal = aantal;
    }

    /**
     * Geeft de code van de factuur.
     *
     * @return De code van de factuur.
     */
    public int getFactuurCode() {
        return factuurCode;
    }

    /**
     * Geeft de code van het onderdeel.
     *
     * @return De code van het onderdeel.
     */
    public int getOnderdeelCode() {
        return onderdeelCode;
    }

    /**
     * Geeft het aantal van het onderdeel.
     *
     * @return Het aantal van het onderdeel.
     */
    public int getAantal() {
        return aantal;
    }
}
