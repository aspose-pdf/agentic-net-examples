using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Empty user password => viewing is allowed without any password
        string userPassword = "";

        // Allow printing and content extraction, but disallow any modifications
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        try
        {
            // Load the PDF, encrypt it, and save the result
            using (Document doc = new Document(inputPath))
            {
                // Encrypt with AES-256; owner password protects editing, user password is empty
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted. Editing requires password. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}