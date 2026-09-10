using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, Permissions, CryptoAlgorithm)

class Program
{
    static void Main()
    {
        // Input PDF to encrypt
        const string inputPath = "input.pdf";

        // Output encrypted PDF
        const string outputPath = "encrypted.pdf";

        // Passwords: user can open the file, owner can change permissions
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
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
                // Set permissions: allow content extraction (copying) but do NOT include PrintDocument
                Permissions perms = Permissions.ExtractContent;

                // Encrypt with AES-256 algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}