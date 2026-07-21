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
            // Load the original PDF
            using (Document doc = new Document(inputPath))
            {
                // Define permissions without the PrintDocument flag (no printing allowed)
                Permissions perms = Permissions.ModifyContent | Permissions.ExtractContent;

                // Encrypt using AES‑256
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            // Verify that the document is encrypted
            using (Document encryptedDoc = new Document(encryptedPath, userPassword))
            {
                Console.WriteLine($"IsEncrypted: {encryptedDoc.IsEncrypted}");
                // Since we set permissions without PrintDocument, printing is disabled.
                Console.WriteLine("Encryption applied with AES‑256 and printing permission disabled.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}