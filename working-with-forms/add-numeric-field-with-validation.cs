using System;

public static class NumericValidator
{
    // Validates that the given numeric value falls within the inclusive range [min, max].
    // Throws ArgumentException if the range definition is invalid (min > max).
    // Returns true if the value is within range; otherwise false.
    public static bool IsValid(int value, int min, int max)
    {
        if (min > max)
            throw new ArgumentException("The minimum value cannot be greater than the maximum value.");

        return value >= min && value <= max;
    }

    // Example usage of the validation logic.
    public static void Main()
    {
        int value = 42;
        int min = 10;
        int max = 100;

        if (IsValid(value, min, max))
            Console.WriteLine($"{value} is within the range [{min}, {max}].");
        else
            Console.WriteLine($"{value} is outside the range [{min}, {max}].");
    }
}