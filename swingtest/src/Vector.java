public class Vector {
    public double x, y;
    
    public Vector(double x, double y) {
        this.x = x; this.y = y;
    }
    
    public Vector plus(Vector other) {
        return new Vector(this.x + other.x, this.y + other.y);
    }
    
    public Vector minus(Vector other) {
        return new Vector(this.x - other.x, this.y - other.y);
    }
    
    public double getLength() {
        return Math.sqrt(x * x + y * y);
    }
    
    public Vector times(double factor) {
        return new Vector(this.x * factor, this.y * factor);
    }
    
    public boolean equals(Vector other) {
        return (this.x == other.x && this.y == other.y);
    }
    
    public static double getAngle(Vector start, Vector end) {
        if (start.equals(end)) {
            return 0;
        }
        
        double opp = start.y - end.y;
        double hyp = start.minus(end).getLength();
        double angle = Math.asin(opp / hyp);
        
        if (start.x > end.x) {
            angle = Math.PI - angle;
        }
        
        return angle;
    }
}