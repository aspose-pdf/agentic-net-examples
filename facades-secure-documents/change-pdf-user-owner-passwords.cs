using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Original owner password of the source PDF
        const string originalOwnerPassword = "oldOwner";

        // New passwords to set
        const string newUserPassword  = "newUser";
        const string newOwnerPassword = "newOwner";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the facade with source and destination files
            using (PdfFileSecurity fileSecurity = new PdfFileSecurity(inputPath, outputPath))
            {
                // Change both user and owner passwords in a single call
                bool changed = fileSecurity.ChangePassword(originalOwnerPassword, newUserPassword, newOwnerPassword);
                Console.WriteLine(changed ? "Passwords changed successfully." : "Password change failed.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}