using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the encrypted output PDF
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";

        // User password (required to open the document) and owner password (required to change permissions)
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Define the permissions you want to allow (e.g., printing and content extraction)
                Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

                // Apply AES‑256 encryption with the specified user and owner passwords
                // Note: CryptoAlgorithm.AESx256 is the recommended algorithm per the encryption rule
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF to the output path
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