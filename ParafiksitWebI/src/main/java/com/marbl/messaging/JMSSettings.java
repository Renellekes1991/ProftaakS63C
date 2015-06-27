package com.marbl.messaging;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.HashMap;
import java.util.Scanner;
import java.util.StringTokenizer;
import java.util.logging.Level;
import java.util.logging.Logger;

public class JMSSettings {

    public static final String CONNECTION = "CONNECTION";
    public static final String CLIENT_ORDER_REQUEST = "CLIENT_ORDER_REQUEST";
    public static final String CLIENT_ORDER_REPLY = "CLIENT_ORDER_REPLY";
    public static final String CLIENT_ORDER_REPLY_2 = "CLIENT_ORDER_REPLY_2";
    public static final String PARAFIKSIT_ORDER_REQUEST = "PARAFIKSIT_ORDER_REQUEST";
    public static final String PARAFIKSIT_ORDER_REPLY = "PARAFIKSIT_ORDER_REPLY";
    public static final String WAREHOUSE_REQUEST = "WAREHOUSE_REQUEST";
    public static final String WAREHOUSE_REPLY = "WAREHOUSE_REPLY";
    public static final String CLIENT_STATUS_REQUEST = "CLIENT_STATUS_REQUEST";
    public static final String CLIENT_STATUS_REPLY = "CLIENT_STATUS_REPLY";
    public static final String CLIENT_STATUS_REPLY_2 = "CLIENT_STATUS_REPLY_2";
    public static final String PARAFIKSIT_STATUS_REQUEST = "PARAFIKSIT_STATUS_REQUEST";
    public static final String PARAFIKSIT_STATUS_REPLY = "PARAFIKSIT_STATUS_REPLY";
    public static final String WAREHOUSE_STATUS_REQUEST = "WAREHOUSE_STATUS_REQUEST";
    public static final String WAREHOUSE_STATUS_REPLY = "WAREHOUSE_STATUS_REPLY";
    private HashMap<String, String> map;

    public JMSSettings(String fileName) {
        File file = new File(fileName);
        System.out.println("Absolute Path: " + file.getAbsolutePath());
        map = new HashMap<>();
        
        try (Scanner scanner = new Scanner(file)) {
            while (scanner.hasNextLine()) {
                String line = scanner.nextLine().replaceAll(" ", "");
                StringTokenizer tk = new StringTokenizer(line, "=");
                String key = tk.nextToken();
                String value = tk.nextToken();
                System.out.println(key + "=" + value);
                map.put(key, value);
            }
        } catch (FileNotFoundException ex) {
            Logger.getLogger(JMSSettings.class.getName()).log(Level.SEVERE, null, ex);
        }
    }

    public String get(String queue) {
        return map.get(queue);
    }
}
