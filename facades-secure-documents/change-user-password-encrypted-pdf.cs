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

        // Existing owner password of the encrypted PDF
        const string ownerPassword = "owner123";

        // New user password you want to set; keep owner password unchanged (null generates a random one)
        const string newUserPassword = "newuser123";
        // Use a regular variable for nullable values – const cannot be null.
        string newOwnerPassword = null; // null or empty keeps the current owner password (or generates a random one)

        // Verify that the input file exists before attempting to bind it.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the PdfFileSecurity facade
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            // Load (bind) the encrypted PDF file
            security.BindPdf(inputPath);

            // Change the user password while preserving all other encryption settings
            bool changed = security.ChangePassword(ownerPassword, newUserPassword, newOwnerPassword);

            // Save the resulting PDF to the output file if the operation succeeded
            if (changed)
            {
                security.Save(outputPath);
                Console.WriteLine("User password changed successfully.");
            }
            else
            {
                Console.WriteLine("Failed to change the password.");
            }
        }
    }
}
