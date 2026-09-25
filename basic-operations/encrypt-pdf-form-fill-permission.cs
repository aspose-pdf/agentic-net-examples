using System;
using System.IO;
using Aspose.Pdf; // CryptoAlgorithm, Permissions, Document are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword  = "user123";   // password required to open the PDF
        const string ownerPassword = "owner123";  // password with full permissions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the source PDF, apply encryption, and save the result
        using (Document doc = new Document(inputPath))
        {
            // Allow only form filling; all other actions (including printing) are denied
            Permissions perms = Permissions.FillForm;

            // Encrypt with AES‑256 (strongest symmetric algorithm supported)
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}