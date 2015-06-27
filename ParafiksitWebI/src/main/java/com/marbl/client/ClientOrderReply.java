/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.client;

import com.marbl.client.domain.PartInfo;
import com.marbl.client.domain.ShippingAddress;
import com.marbl.client.domain.WorkPerformedInfo;
import java.io.Serializable;
import java.math.BigDecimal;
import java.util.ArrayList;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
public class ClientOrderReply  implements Serializable{
    
    private String string;
    
    private String nameClient;
    private ShippingAddress shippingAddress;
    private String reparationDescription;
    private ArrayList<WorkPerformedInfo> workPerformedInfo;
    private BigDecimal totalForWorkPerformed;
    private ArrayList<PartInfo> partInfo;
    private BigDecimal totalForParts;
    private BigDecimal total;
    private String BankAccount;
    
    public ClientOrderReply(String s){
        string = s;
    }

    public ClientOrderReply(String nameClient, ShippingAddress shippingAddress, String reparationDescription, 
            ArrayList<WorkPerformedInfo> workPerformedInfo, BigDecimal totalForWorkPerformed, ArrayList<PartInfo> partInfo, 
            BigDecimal totalForParts, BigDecimal total, String BankAccount) {
        this.nameClient = nameClient;
        this.shippingAddress = shippingAddress;
        this.reparationDescription = reparationDescription;
        this.workPerformedInfo = workPerformedInfo;
        this.totalForWorkPerformed = totalForWorkPerformed;
        this.partInfo = partInfo;
        this.totalForParts = totalForParts;
        this.total = total;
        this.BankAccount = BankAccount;
    }

    public String getNameClient()
    {
        return nameClient;
    }

    public String getBankAccount()
    {
        return BankAccount;
    }

    public ArrayList<PartInfo> getPartInfo()
    {
        return partInfo;
    }

    public String getReparationDescription()
    {
        return reparationDescription;
    }

    public ShippingAddress getShippingAddress()
    {
        return shippingAddress;
    }

    public BigDecimal getTotal()
    {
        return total;
    }

    public BigDecimal getTotalForParts()
    {
        return totalForParts;
    }

    public BigDecimal getTotalForWorkPerformed()
    {
        return totalForWorkPerformed;
    }

    public ArrayList<WorkPerformedInfo> getWorkPerformedInfo()
    {
        return workPerformedInfo;
    }
    

    
    
    public String getString()
    {
        return string;
    }
    
    public void setString(String s){
        string = s;
    }
}
