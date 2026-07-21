using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, Permissions, CryptoAlgorithm)

class Program
{
    static void Main()
    {
        // Input PDF to encrypt
        const string inputPath  = "input.pdf";
        // Output encrypted PDF
        const string outputPath = "encrypted_rc4_128.pdf";

        // Passwords (user password for opening, owner password for full permissions)
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        // Define permissions: allow printing only, deny editing (ModifyContent, FillForm, etc.)
        Permissions permissions = Permissions.PrintDocument;

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Apply RC4 128‑bit encryption with the specified permissions
                doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);

                // Save the encrypted document
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF encrypted successfully and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during encryption: {ex.Message}");
        }
    }
}