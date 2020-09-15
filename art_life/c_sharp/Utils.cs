using System;
using System.Collections.Generic;

public static class Utils {
    public static readonly Random Rnd = new Random();
    
    public static T Random_Element<T>(T[] elements) {
        return elements[Rnd.Next(0, elements.Length)];
    }
    
    public static bool Chance(double chance) {
        return Rnd.NextDouble() < chance;
    }
}