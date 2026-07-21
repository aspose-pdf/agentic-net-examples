using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (must be encrypted with an owner password)
        const string inputPath = "input.pdf";
        // Output PDF with the new owner password
        const string outputPath = "output.pdf";

        // Original owner password (required to authorize the change)
        const string currentOwnerPassword = "oldOwnerPass";
        // New owner password to set
        const string newOwnerPassword = "newOwnerPass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade with source and destination files
            using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
            {
                // Change only the owner password.
                // Pass null or empty string for newUserPassword to keep the existing user password unchanged.
                // The method preserves all existing privileges.
                bool changed = security.ChangePassword(currentOwnerPassword, null, newOwnerPassword);

                if (changed)
                {
                    Console.WriteLine($"Owner password updated successfully. Output saved to '{outputPath}'.");
                }
                else
                {
                    Console.Error.WriteLine("Failed to change the owner password.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}