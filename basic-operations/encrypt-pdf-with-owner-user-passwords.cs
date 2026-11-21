using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF and apply encryption
            using (Document doc = new Document(inputPath))
            {
                // Restrict permissions: do NOT include PrintDocument permission
                Permissions perms = Permissions.ExtractContent; // example: allow content extraction only
                // Encrypt with AES-256 (recommended)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}