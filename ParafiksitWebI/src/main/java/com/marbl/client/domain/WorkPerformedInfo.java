package com.marbl.client.domain;

import java.io.Serializable;

public class WorkPerformedInfo implements Serializable {

    private String description;

    public WorkPerformedInfo(String description) {
        this.description = description;
    }

    public String getDescription() {
        return description;
    }
    
    public void setDescription(String description) {
        this.description = description;
    }
}
