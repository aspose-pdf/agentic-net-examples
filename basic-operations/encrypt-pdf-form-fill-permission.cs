using System;
using System.IO;
using Aspose.Pdf; // Core API namespace (contains Document, Permissions, CryptoAlgorithm)

class Program
{
    static void Main()
    {
        // Input PDF path
        const string inputPath = "input.pdf";
        // Output encrypted PDF path
        const string outputPath = "encrypted.pdf";

        // User password (required to open the document)
        const string userPassword = "UserPass123";
        // Owner password (required to change permissions later)
        const string ownerPassword = "OwnerPass123";

        // Verify the source file exists
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
                // Set permissions: allow only form filling, no printing or other actions
                Permissions perms = Permissions.FillForm;

                // Encrypt the document with the specified passwords, permissions, and algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
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