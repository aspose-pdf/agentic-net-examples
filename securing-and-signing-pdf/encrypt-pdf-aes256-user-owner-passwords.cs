using System;
using System.IO;
using Aspose.Pdf; // CryptoAlgorithm, Permissions, Document

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output encrypted PDF path
        const string outputPath = "encrypted_aes256.pdf";

        // User password (required to open the document)
        const string userPassword = "user123";
        // Owner password (required to change permissions)
        const string ownerPassword = "owner123";

        // Verify input file exists
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
                // Define permissions you want to allow.
                // Here we allow printing and content extraction.
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Encrypt using AES‑256 algorithm.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted with AES‑256 and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}