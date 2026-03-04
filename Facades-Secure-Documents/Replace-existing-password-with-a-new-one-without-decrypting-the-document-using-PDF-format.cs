using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldOwnerPassword = "oldOwner";
        const string newUserPassword = "newUser";
        const string newOwnerPassword = "newOwner";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the facade with source and destination files
        PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

        // Change the passwords while keeping the original security settings
        bool changed = fileSecurity.ChangePassword(oldOwnerPassword, newUserPassword, newOwnerPassword);

        if (!changed)
        {
            Console.Error.WriteLine("Failed to change password.");
        }
        else
        {
            Console.WriteLine($"Password changed successfully. Output saved to '{outputPath}'.");
        }

        // No explicit Save call is required; ChangePassword writes the result to outputPath
    }
}