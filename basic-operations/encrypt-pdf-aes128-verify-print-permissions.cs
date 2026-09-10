using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Set permissions: allow printing and high‑quality printing
                Permissions perms = (Permissions)(Permissions.PrintDocument | Permissions.PrintingQuality);

                // Encrypt using AES‑128
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            // Verify the encryption settings
            using (Document encDoc = new Document(encryptedPath, ownerPassword))
            {
                // Retrieve the permissions stored in the encryption dictionary (cast to Permissions)
                Permissions actualPerms = (Permissions)encDoc.Permissions;

                bool canPrint = (actualPerms & Permissions.PrintDocument) == Permissions.PrintDocument;
                bool highQuality = (actualPerms & Permissions.PrintingQuality) == Permissions.PrintingQuality;

                Console.WriteLine($"Encryption verified. PrintDocument: {canPrint}, PrintingQuality: {highQuality}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
