using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string currentOwnerPassword = "ownerPass";   // original owner password
        const string newUserPassword = "newUserPass";      // new user password
        const string newOwnerPassword = null;              // null/empty generates a random owner password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with source and destination files
            using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
            {
                // Change the user password while keeping existing encryption settings
                bool changed = security.ChangePassword(currentOwnerPassword, newUserPassword, newOwnerPassword);
                if (!changed)
                {
                    Console.Error.WriteLine("Failed to change the password.");
                }
                else
                {
                    Console.WriteLine($"Password changed successfully. Output saved to '{outputPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}