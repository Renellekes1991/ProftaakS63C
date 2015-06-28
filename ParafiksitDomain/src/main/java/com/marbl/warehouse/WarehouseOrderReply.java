/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.warehouse;

import com.marbl.warehouse.domain.Factuur;
import java.io.Serializable;
import java.math.BigDecimal;


public class WarehouseOrderReply  implements Serializable
{
    private Factuur factuur;
    private BigDecimal totalPriceForParts;

    public WarehouseOrderReply(Factuur factuur)
    {
        this.factuur = factuur;
        totalPriceForParts = new BigDecimal(0);
    }

    public Factuur getFactuur()
    {
        return factuur;
    }

    public void setFactuur(Factuur factuur)
    {
        this.factuur = factuur;
    }
}
