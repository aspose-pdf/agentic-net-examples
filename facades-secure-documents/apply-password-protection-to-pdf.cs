using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF (must exist) and output PDF paths
        const string inputPath  = "input.pdf";
        const string outputPath = "protected_output.pdf";

        // Passwords to apply
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Initialize the PdfFileSecurity facade with source and destination files
            PdfFileSecurity security = new PdfFileSecurity(inputPath, outputPath);

            // Apply encryption:
            //   - userPassword: password required to open the document
            //   - ownerPassword: password required to change permissions
            //   - DocumentPrivilege.Print: allow printing only (other actions are denied)
            //   - KeySize.x256: use 256‑bit AES encryption (strongest supported)
            bool success = security.EncryptFile(
                userPassword,
                ownerPassword,
                DocumentPrivilege.Print,
                KeySize.x256);

            if (success)
                Console.WriteLine($"PDF encrypted successfully and saved to '{outputPath}'.");
            else
                Console.Error.WriteLine("Encryption failed (method returned false).");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during encryption: {ex.Message}");
        }
    }
}