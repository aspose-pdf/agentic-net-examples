using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string oldOwnerPassword = "oldOwnerPassword";
        const string newOwnerPassword = "newOwnerPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize PdfFileSecurity with source and destination files.
            using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
            {
                // Change only the owner password.
                // Pass null for newUserPassword to keep the existing user password unchanged.
                bool changed = security.ChangePassword(oldOwnerPassword, null, newOwnerPassword);
                if (!changed)
                {
                    Console.Error.WriteLine("Failed to update the owner password.");
                }
                else
                {
                    Console.WriteLine($"Owner password updated successfully. Output saved to '{outputPath}'.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}