package com.marbl.parafiksitwebi.beans;

import com.marbl.client.ClientOrderRequest;
import com.marbl.client.domain.PartInfo;
import com.marbl.client.domain.WorkPerformedInfo;
import com.marbl.messaging.JMSSettings;
import com.marbl.parafiksitwebi.messaging.ClientTest;
import java.io.Serializable;
import javax.annotation.PostConstruct;
import javax.enterprise.context.SessionScoped;
import javax.faces.context.ExternalContext;
import javax.faces.context.FacesContext;
import javax.inject.Named;

@Named
@SessionScoped
public class OrderBean implements Serializable {

    private ClientOrderRequest request;
    private ClientTest client;
    private String operationDescriptions;
    private String partNames;
    
    @PostConstruct
    public void postConstruct() {
        try
        {
        ExternalContext externalContext = FacesContext.getCurrentInstance().getExternalContext();
        JMSSettings queueNames = new JMSSettings(externalContext.getRealPath("/../../src/main/resources/MESSAGING_CHANNELS.ini"));
        final String factoryName = queueNames.get(JMSSettings.CONNECTION);
        final String clientOrderRequestQueue = queueNames.get(JMSSettings.CLIENT_ORDER_REQUEST);
        final String clientOrder2ReplyQueue = queueNames.get(JMSSettings.CLIENT_ORDER_REPLY_2);

        client = new ClientTest("FontysApp", factoryName, clientOrderRequestQueue, clientOrder2ReplyQueue);
        client.start();
        request = new ClientOrderRequest();
        System.out.println("Init done.");
        } catch(Exception ec) {
            System.out.println("Init done. FAILED");
        }
    }

    public ClientOrderRequest getRequest() {
        return request;
    }

    public void setRequest(ClientOrderRequest request) {
        this.request = request;
    }

    public String getOperationDescriptions() {
        return operationDescriptions;
    }

    public void setOperationDescriptions(String operationDescriptions) {
        this.operationDescriptions = operationDescriptions;
    }

    public String getPartNames() {
        return partNames;
    }

    public void setPartNames(String partNames) {
        this.partNames = partNames;
    }

    public String order() {
        for (String operationDescription : operationDescriptions.split(",")) {
            request.getOperations().add(new WorkPerformedInfo(operationDescription));
        }
        
        for (String partName : partNames.split(",")) {
            request.getParts().add(new PartInfo(partName));
        }
        
        System.out.println("Sending request.");
        client.sendOrderRequest(request);
        
        return "";
    }
}