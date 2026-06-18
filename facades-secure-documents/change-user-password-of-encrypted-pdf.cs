using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source (encrypted) PDF and the destination PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Original owner password (required to authorize the change)
        const string ownerPassword = "owner123";

        // New user password you want to set; keep the owner password unchanged (null generates a random one)
        const string newUserPassword = "newUser123";
        const string newOwnerPassword = null; // null or empty => random owner password

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // ---------- Lifecycle: create ----------
            // Create a PdfFileSecurity instance (no parameters)
            PdfFileSecurity security = new PdfFileSecurity();

            // ---------- Lifecycle: load ----------
            // Bind the encrypted PDF file to the facade
            security.BindPdf(inputPath);

            // ---------- Operation ----------
            // Change only the user password while preserving all other encryption settings
            bool changed = security.ChangePassword(ownerPassword, newUserPassword, newOwnerPassword);

            if (!changed)
            {
                Console.Error.WriteLine("Password change failed.");
                return;
            }

            // ---------- Lifecycle: save ----------
            // Save the resulting PDF with the new password
            security.Save(outputPath);

            Console.WriteLine($"User password changed successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}