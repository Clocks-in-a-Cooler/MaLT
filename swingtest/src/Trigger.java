import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

public class Trigger implements ActionListener {
    public Trigger() {
        //empty
    }
    
    public void actionPerformed(ActionEvent e) {
        for (Bubble b : SwingTest.bubbles) {
            b.update();
        }
        SwingTest.panel.repaint();
    }
}