using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";   // Encrypted source PDF
        const string outputPath = "output.pdf";  // PDF with updated user password
        const string ownerPassword   = "ownerPass";   // Existing owner password
        const string newUserPassword = "newUserPass"; // Desired new user password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with source and destination files
            PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath);

            // Change only the user password; keep the owner password unchanged
            bool changed = fileSecurity.ChangePassword(ownerPassword, newUserPassword, ownerPassword);

            if (!changed)
            {
                Console.Error.WriteLine("Failed to change the password.");
            }
            else
            {
                Console.WriteLine($"User password updated successfully. Output saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}