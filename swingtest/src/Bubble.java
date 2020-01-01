import java.awt.Color;
import java.awt.Graphics;
import java.util.Random;

public class Bubble {
    public Vector pos;
    public int radius;
    public Vector motion;
    public Color colour;
    public static double bounciness = 0.015;
    
    public Bubble(int x, int y, int radius) {
        this.pos    = new Vector(x, y);
        this.radius = radius;
        Random rnd  = new Random();
        Vector motion = new Vector(rnd.nextInt(2) + 1, rnd.nextInt(2) + 1);
        if (rnd.nextInt(2) == 1) {
            motion.x *= -1;
        }
        
        if (rnd.nextInt(2) == 1) {
            motion.y *= -1;
        }
        
        this.motion = motion;
        this.colour = new Color(rnd.nextInt(256), rnd.nextInt(256), rnd.nextInt(256), 128);
    }
    
    public void update() {
        //assume 20 ms have gone by
        this.pos = this.pos.plus(this.motion.times(1));
        
        //collision detection
        if (this.pos.x <= this.radius + SwingTest.border || this.pos.x >= SwingTest.width - this.radius - SwingTest.border) {
            this.motion.x *= -1;
        }
        
        if (this.pos.y <= this.radius + SwingTest.border || this.pos.y >= SwingTest.height - this.radius - SwingTest.border) {
            this.motion.y *= -1;
        }
        
        //detection with each other
        for (Bubble b : SwingTest.bubbles) {
            if (b == this) {
                continue;
            }
            
            double minDist = this.radius + b.radius;
            double distance = this.pos.minus(b.pos).getLength();
            double overlap = minDist - distance;
            if (overlap > 0) {
                double angle = Vector.getAngle(this.pos, b.pos);
                this.motion.x += Math.cos(angle) * (overlap * bounciness);
                this.motion.y += Math.sin(angle) * (overlap * bounciness);
            }
        }
    }
    
    public void draw(Graphics g) {
        g.setColor(this.colour);
        g.fillOval((int)(this.pos.x - this.radius), (int)(this.pos.y - this.radius), this.radius * 2, this.radius * 2);
    }
}