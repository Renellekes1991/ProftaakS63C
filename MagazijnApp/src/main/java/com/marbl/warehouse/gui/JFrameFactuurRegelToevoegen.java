package com.marbl.warehouse.gui;

import com.marbl.warehouse.domain.Onderdeel;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;
import java.util.Collection;
import javax.swing.JButton;
import javax.swing.JComboBox;
import javax.swing.JLabel;
import javax.swing.JOptionPane;
import javax.swing.JTextField;
import javax.swing.WindowConstants;

public class JFrameFactuurRegelToevoegen extends javax.swing.JFrame implements ActionListener {

    private JFrameToevoegen main;
    private JComboBox list;
    private ArrayList<Onderdeel> onderdelen;
    private JTextField tf;

    /**
     * Creates new form JFrameFactuurRegelToevoegen
     */
    public JFrameFactuurRegelToevoegen(JFrameToevoegen main, Collection<Onderdeel> onderdelen) {
        initComponents();
        this.setLocation(400, 250);
        this.setDefaultCloseOperation(WindowConstants.DO_NOTHING_ON_CLOSE);
        this.setTitle("Regel Toevoegen");
        this.main = main;
        this.onderdelen = new ArrayList(onderdelen);

        list = new JComboBox();
        list.setBounds(20, 20, 190, 20);
        add(list);
        for (Onderdeel onderdeel : onderdelen) {
            list.addItem(onderdeel.getCode() + ":  " + onderdeel.getOmschrijving());

        }

        JLabel lbl = new JLabel("Aantal:");
        lbl.setBounds(20, 50, 80, 30);
        lbl.setFont(new Font("Times New Roman", 0, 15));
        add(lbl);

        tf = new JTextField();
        tf.setBounds(110, 50, 80, 20);
        tf.setFont(new Font("Times New Roman", 0, 15));
        add(tf);

        JButton btVoeg = new JButton("Voeg Toe");
        btVoeg.setBounds(10, 80, 100, 20);
        btVoeg.setFont(new Font("Times New Roman", 0, 15));
        btVoeg.addActionListener(this);
        btVoeg.setActionCommand("Voeg Toe");
        add(btVoeg);

        JButton btCancel = new JButton("Cancel");
        btCancel.setBounds(120, 80, 100, 20);
        btCancel.setFont(new Font("Times New Roman", 0, 15));
        btCancel.addActionListener(this);
        btCancel.setActionCommand("Cancel");
        add(btCancel);
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setName("Form"); // NOI18N

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 232, Short.MAX_VALUE)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 123, Short.MAX_VALUE)
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    // Variables declaration - do not modify//GEN-BEGIN:variables
    // End of variables declaration//GEN-END:variables
    @Override
    public void actionPerformed(ActionEvent e) {
        switch (e.getActionCommand()) {
            case "Cancel": {
                this.setVisible(false);
                main.setVisible(true);
                this.dispose();
                break;
            }
            case "Voeg Toe": {
                int aantal = Integer.parseInt(tf.getText());
                if (aantal > 0) {
                    if (aantal <= onderdelen.get(list.getSelectedIndex()).getAantal()) {
                        String temp = Integer.toString(aantal) + "x: " + onderdelen.get(list.getSelectedIndex()).getOmschrijving() + "(" + Integer.toString(onderdelen.get(list.getSelectedIndex()).getCode()) + ")";
                        onderdelen.get(list.getSelectedIndex()).setAantal(aantal);
                        main.giveString(temp, onderdelen.get(list.getSelectedIndex()));
                        this.setVisible(false);
                        main.setVisible(true);
                        this.dispose();
                    } else {
                        JOptionPane.showMessageDialog(this, "Het gewenste aantal moet lager dan het aanwezige aantal zijn.", "Waarschuwing", JOptionPane.WARNING_MESSAGE);
                    }
                } else {
                    JOptionPane.showMessageDialog(this, "Het gewenste aantal moet hoger dan nul zijn.", "Waarschuwing", JOptionPane.WARNING_MESSAGE);
                }
                break;
            }
        }
    }
}