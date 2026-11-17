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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF, apply RC4 128‑bit encryption and restrict editing.
            using (Document doc = new Document(inputPath))
            {
                // Allow printing and content extraction, but disallow modifications.
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using RC4 128‑bit algorithm.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);

                // Save the encrypted PDF.
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