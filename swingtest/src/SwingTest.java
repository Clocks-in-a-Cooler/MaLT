import javax.swing.JFrame;
import javax.swing.Timer;
import java.awt.Dimension;
import java.util.ArrayList;

public class SwingTest {
    public static JFrame window;
    public static ArrayList<Bubble> bubbles;
    public static int delay = 20;
    public static Timer timer;
    public static BubblePanel panel;
    public static int width = 450, height = 450;
    public static int border = 3;
    
    public static void main(String[] args) {
        window = new JFrame("Bubbles!");
        window.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        panel = new BubblePanel();
        window.getContentPane().add(panel);
        window.setSize(new Dimension(width + 5, height + 5));
        window.setVisible(true);
        
        bubbles = new ArrayList<Bubble>();
        
        timer = new Timer(delay, new Trigger());
        timer.start();
    }
}