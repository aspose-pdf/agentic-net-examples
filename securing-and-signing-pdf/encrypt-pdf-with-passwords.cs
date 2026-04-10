using System;
using System.IO;
using Aspose.Pdf; // CryptoAlgorithm, Permissions, Document are in this namespace

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";

        // Passwords required to open (user) and to change permissions (owner)
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF, encrypt it, and save the encrypted version
            using (Document doc = new Document(inputPath))
            {
                // Define allowed operations for the user (e.g., print and extract)
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES-256 (recommended) and the specified passwords
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