using System;
using System.IO;
using Aspose.Pdf; // CryptoAlgorithm, Permissions, Document are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword  = "";          // empty user password (allows opening without password)
        const string ownerPassword = "owner123"; // owner password to protect permissions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Allow printing (including high‑resolution) but restrict copying/extraction
                Permissions perms = Permissions.PrintDocument | Permissions.PrintingQuality;

                // Encrypt using AES‑256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
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