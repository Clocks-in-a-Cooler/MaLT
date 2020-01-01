import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.util.Random;

class BubblePanel extends JPanel implements MouseListener {
    BubblePanel() {
        super();
        setBackground(Color.WHITE);
        repaint();
        addMouseListener(this);
    }
    
    @Override
    public void paintComponent(Graphics g) {
        super.paintComponent(g);
        for (Bubble b : SwingTest.bubbles) {
            b.draw(g);
        }
    }
    /* METHODS TO IMPLEMENT:
     * [x] mouseClicked
     * [ ] mouseEntered
     * [ ] mouseExited
     * [ ] mousePressed
     * [ ] mouseReleased
     */
    public void mouseClicked(MouseEvent e) {
        int x = e.getX();
        int y = e.getY();
        Random rnd = new Random();
        
        SwingTest.bubbles.add(new Bubble(x, y, rnd.nextInt(15) + 10));
        setBackground(Color.WHITE);
        
        repaint();
    }
    
    public void mouseEntered(MouseEvent e) {
        
    }
    
    public void mouseExited(MouseEvent e) {
        
    }
    
    public void mousePressed(MouseEvent e) {
        
    }
    
    public void mouseReleased(MouseEvent e) {
        
    }
}