using System;
using System.IO;
using Aspose.Pdf; // CryptoAlgorithm, Permissions, Document are in this namespace

class Program
{
    static void Main()
    {
        // Input PDF to encrypt
        const string inputPath = "input.pdf";
        // Output encrypted PDF
        const string outputPath = "encrypted.pdf";

        // User (open) password and owner (full‑access) password
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document doc = new Document(inputPath))
            {
                // Define desired permissions (example: allow printing and content extraction)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt the document using AES‑256 (encryption-always-use-CryptoAlgorithm rule)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF (lifecycle rule: use Document.Save)
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