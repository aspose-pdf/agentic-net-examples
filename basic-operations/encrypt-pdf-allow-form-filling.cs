using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace contains Document, Permissions, CryptoAlgorithm

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "encrypted.pdf";  // destination encrypted PDF
        const string userPassword  = "user123";    // password required to open the PDF
        const string ownerPassword = "owner123";   // password with full permissions

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document (lifecycle: load)
            using (Document doc = new Document(inputPath))
            {
                // Define permissions: allow only form filling, no content extraction
                Permissions perms = Permissions.FillForm;

                // Encrypt the document using AES‑256 (encryption rule)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF (lifecycle: save)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}