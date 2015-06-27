package com.marbl.warehouse;

import com.marbl.messaging.JMSSettings;
import com.marbl.warehouse.gui.Magazijn;
import java.sql.SQLException;
import org.jdesktop.application.Application;
import org.jdesktop.application.SingleFrameApplication;

public class MagazijnApplicatieApp extends SingleFrameApplication {

    /**
     * At startup create and show the main frame of the application.
     */
    @Override
    protected void startup() {
        try {
            show(new Magazijn());
            // read the queue names from file "MESSAGING.ini"  
            JMSSettings queueNames = new JMSSettings("src/main/resources/MESSAGING_CHANNELS.ini");
            final String factoryName = queueNames.get(JMSSettings.CONNECTION);
            //CLIENTS & BROKER
//            final String clientRequestQueue = queueNames.get(JMSSettings.CLIENT_REQUEST);
//            final String clientReplyQueue = queueNames.get(JMSSettings.CLIENT_REPLY);
//            final String client2ReplyQueue = queueNames.get(JMSSettings.CLIENT_REPLY_2);
//            //PARAFIKSIT & BROKER
//            final String parafiksitRequestQueue = queueNames.get(JMSSettings.PARAFIKSIT_REQUEST);
//            final String parafiksitReplyQueue = queueNames.get(JMSSettings.PARAFIKSIT_REPLY);
            //WAREHOUSE & BROKER
            final String warehouseRequestQueue = queueNames.get(JMSSettings.WAREHOUSE_REQUEST);
            final String warehouseReplyQueue = queueNames.get(JMSSettings.WAREHOUSE_REPLY);
            
            WarehouseMessaging warehouse = new WarehouseMessaging(factoryName, warehouseRequestQueue, warehouseReplyQueue);
            warehouse.start();
        } catch (SQLException ex) {
            System.err.println(ex);
            exit();
        }
    }

    /**
     * This method is to initialize the specified window by injecting resources.
     * Windows shown in our application come fully initialized from the GUI
     * builder, so this additional configuration is not needed.
     */
    @Override
    protected void configureWindow(java.awt.Window root) {
    }

    /**
     * A convenient static getter for the application instance.
     *
     * @return the instance of MagazijnApplicatieApp
     */
    public static MagazijnApplicatieApp getApplication() {
        return Application.getInstance(MagazijnApplicatieApp.class);
    }

    /**
     * Main method launching the application.
     */
    public static void main(String[] args) {
        launch(MagazijnApplicatieApp.class, args);
    }
}
