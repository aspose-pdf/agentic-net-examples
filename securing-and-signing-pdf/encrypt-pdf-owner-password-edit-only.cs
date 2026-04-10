using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // Owner password required for editing; no user password needed for viewing
        const string ownerPassword = "owner123";
        const string userPassword  = ""; // empty => view without password

        // Allow only printing; all other modifications are prohibited
        Permissions permissions = Permissions.PrintDocument;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF and encrypt it
            using (Document doc = new Document(inputPath))
            {
                // Encrypt using AES-256; user can view without password, editing requires owner password
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);
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