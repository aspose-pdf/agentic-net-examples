using System;
using System.Text.RegularExpressions;

class Program
{
    // Regular expression for a valid IPv4 address.
    // Each octet: 0-255, no leading zeros unless the octet is zero.
    private static readonly Regex IPv4Regex = new Regex(
        @"^(?:(?:25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\.){3}"
      + @"(?:25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)$",
        RegexOptions.Compiled);

    // Returns true if the input string is a valid IPv4 address.
    static bool IsValidIPv4(string ipAddress)
    {
        if (string.IsNullOrWhiteSpace(ipAddress))
            return false;

        return IPv4Regex.IsMatch(ipAddress);
    }

    static void Main()
    {
        // Example usage: validate a sample IP address.
        string[] testAddresses = {
            "192.168.1.1",
            "255.255.255.255",
            "0.0.0.0",
            "256.100.50.25",   // invalid
            "123.045.067.089", // invalid (leading zeros)
            "abc.def.ghi.jkl"  // invalid
        };

        foreach (var ip in testAddresses)
        {
            Console.WriteLine($"{ip} => {(IsValidIPv4(ip) ? "Valid" : "Invalid")}");
        }
    }
}