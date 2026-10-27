using System;

public static class NumericValidator
{
    /// <summary>
    /// Validates that a numeric value lies within the specified inclusive range.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="min">Minimum acceptable value (inclusive).</param>
    /// <param name="max">Maximum acceptable value (inclusive).</param>
    /// <returns>True if the value is within range; otherwise false.</returns>
    /// <exception cref="ArgumentException">Thrown when min is greater than max.</exception>
    public static bool IsWithinRange(double value, double min, double max)
    {
        if (min > max)
            throw new ArgumentException("Minimum value cannot be greater than maximum value.");

        return value >= min && value <= max;
    }

    /// <summary>
    /// Example usage of the validator.
    /// </summary>
    public static void Example()
    {
        double input = 42.5;
        double minAllowed = 0.0;
        double maxAllowed = 100.0;

        if (IsWithinRange(input, minAllowed, maxAllowed))
        {
            Console.WriteLine($"{input} is within the acceptable range [{minAllowed}, {maxAllowed}].");
        }
        else
        {
            Console.WriteLine($"{input} is outside the acceptable range [{minAllowed}, {maxAllowed}].");
        }
    }
}

public class Program
{
    public static void Main()
    {
        // Entry point for the console application.
        NumericValidator.Example();
    }
}
