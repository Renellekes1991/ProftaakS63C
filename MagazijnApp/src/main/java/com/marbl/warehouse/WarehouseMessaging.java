/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.marbl.warehouse;

import com.marbl.client.domain.PartInfo;
import com.marbl.warehouse.domain.Database;
import com.marbl.warehouse.domain.Factuur;
import com.marbl.warehouse.domain.FactuurRegel;
import com.marbl.warehouse.domain.Klant;
import com.marbl.warehouse.domain.Onderdeel;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Date;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 *
 * Nick Wondergem & Danik Raikhlin
 */
public class WarehouseMessaging
{

    private ComputerBrokerGateway gateway;
    private Database db;

    public WarehouseMessaging(String factoryName, String requestQueue, String replyQueue)
    {
        gateway = new ComputerBrokerGateway(factoryName, requestQueue, replyQueue)
        {
            @Override
            void receivedWarehouseRequest(WarehouseOrderRequest request)
            {
                try
                {
                    db = new Database();

                    //First, create a new Klant object...
                    int klantID = 0;
                    for (Klant k : db.selectKlanten())
                    {
                        if (klantID <= k.getCode())
                        {
                            klantID = k.getCode() + 1;
                        }
                    }
                    String address = request.getShipping().getStreet() + " " + request.getShipping().getNumber() + ", " + request.getShipping().getPlace();
                    Klant klant = new Klant(klantID, request.getContact().getContactName(), address);
                    
                    
                    //Next, make the factuur object
                    int factuurId = 0;
                    for (Factuur f : db.selectFacturen())
                    {
                        if (f.getCode() > factuurId)
                        {
                            factuurId = f.getCode();
                        }
                    }
                    //And for each part you need, make a new Factuurregel
                    ArrayList<FactuurRegel> regels = new ArrayList<FactuurRegel>();                 
                    
                    //Check what id is the next Id, the total price and
                    int onderdeelId = 0;
                    int totalPrice = 0;
                    
                    ArrayList<Onderdeel> onderdelen = (ArrayList<Onderdeel>) db.selectOnderdelen();
                    
                    //Check for each part in your request...
                    for (PartInfo i : request.getParts())
                    {
                        //against all the parts in database
                        for(Onderdeel o : onderdelen)
                        {
                            //If the name 
                            if(i.getName().equals(o.getOmschrijving()))
                            {
                                onderdeelId = o.getCode();
                                totalPrice += o.getPrijs();
                            }
                        }
                        regels.add(new FactuurRegel(factuurId, onderdeelId, 1));
                    }
                    
                    Factuur f = new Factuur(factuurId, klant.getCode(), new Date().toString(), regels);
                    f.setRegels(regels);
                    
                    gateway.sendReply(request, new WarehouseOrderReply(f));

                    System.out.println("I sent away a reply from the warehouse. I needed " + request.getParts().get(0));
                } catch (SQLException ex)
                {
                    Logger.getLogger(WarehouseMessaging.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
        };
    }

    public void start()
    {
        gateway.start();
    }
}
