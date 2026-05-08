using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Owner password to protect the document.
        const string ownerPassword = "owner123";
        // Empty user password – users can open the PDF without a password.
        const string userPassword = "";

        // Permissions: allow printing (including high‑resolution) and disallow copying.
        Permissions perms = Permissions.PrintDocument | Permissions.PrintingQuality;

        try
        {
            // Load the PDF, encrypt it, and save the result.
            using (Document doc = new Document(inputPath))
            {
                // Encrypt using AES‑256 algorithm.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
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