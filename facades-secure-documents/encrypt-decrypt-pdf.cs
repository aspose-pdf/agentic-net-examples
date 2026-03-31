using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the original PDF and encrypt it
            using (Document originalDoc = new Document(inputPath))
            {
                Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;
                originalDoc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);
                originalDoc.Save(encryptedPath);
                Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
            }

            // Open the encrypted PDF with the user password, decrypt it, and save the result
            using (Document encryptedDoc = new Document(encryptedPath, userPassword))
            {
                encryptedDoc.Decrypt();
                encryptedDoc.Save(decryptedPath);
                Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
            }

            // Verify round‑trip integrity by comparing page counts
            using (Document originalDoc = new Document(inputPath))
            using (Document decryptedDoc = new Document(decryptedPath))
            {
                int originalPageCount = originalDoc.Pages.Count;
                int decryptedPageCount = decryptedDoc.Pages.Count;
                if (originalPageCount == decryptedPageCount)
                {
                    Console.WriteLine("Round‑trip integrity verified: page count matches.");
                }
                else
                {
                    Console.WriteLine($"Integrity check failed: original pages {originalPageCount}, decrypted pages {decryptedPageCount}.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
