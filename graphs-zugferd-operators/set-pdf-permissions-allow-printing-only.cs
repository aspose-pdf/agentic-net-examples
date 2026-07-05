using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_protected.pdf";
        const string userPassword = "";          // empty user password (can be set as needed)
        const string ownerPassword = "owner123"; // owner password controls permission changes

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Set permissions: allow printing only
                Permissions perms = Permissions.PrintDocument;
                // Encrypt with AES-256 (recommended) and the specified permissions
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                // Save the protected PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with restricted permissions to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}