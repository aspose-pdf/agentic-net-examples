using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input and encrypted output PDFs
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";

        // Passwords for user (open) and owner (full control)
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Define permissions: allow printing and content extraction
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Apply AES‑256 encryption with the specified passwords and permissions
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPath);
            }

            Console.WriteLine($"Encryption successful. Encrypted file saved to '{encryptedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during encryption: {ex.Message}");
        }
    }
}