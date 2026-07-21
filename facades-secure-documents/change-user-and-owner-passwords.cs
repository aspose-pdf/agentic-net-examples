using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Current owner password of the source PDF
        const string currentOwnerPassword = "oldOwner";

        // New passwords to set
        const string newUserPassword = "newUser";
        const string newOwnerPassword = "newOwner";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with input and output files
            using (PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath))
            {
                // Change both user and owner passwords in a single call
                bool success = security.ChangePassword(currentOwnerPassword, newUserPassword, newOwnerPassword);
                Console.WriteLine(success ? "Password change succeeded." : "Password change failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}