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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source PDF and encrypt it with printing permission only
            using (Document doc = new Document(inputPath))
            {
                // Allow only printing (no other permissions)
                Permissions perms = Permissions.PrintDocument;

                // Encrypt using AES-256 (preferred algorithm)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"PDF encrypted successfully. Output: {encryptedPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}