using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        // Example IP address to validate
        string ip = "192.168.0.1";

        bool isValid = IsValidIPv4(ip);
        Console.WriteLine($"{ip} is {(isValid ? "valid" : "invalid")} IPv4 address.");
    }

    // Returns true if the input string matches IPv4 format (0.0.0.0 to 255.255.255.255)
    static bool IsValidIPv4(string ip)
    {
        if (string.IsNullOrWhiteSpace(ip))
            return false;

        // Regex pattern for IPv4 octets ranging from 0 to 255
        const string pattern = @"^(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)(\.(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)){3}$";

        return Regex.IsMatch(ip, pattern);
    }
}