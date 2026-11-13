using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "secured.pdf";

        // Passwords for encryption – can be empty strings if no password is required
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

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
                // Set permissions: allow printing only.
                // Do NOT include ExtractContent (copy) or ModifyAnnotations.
                Permissions perms = Permissions.PrintDocument;

                // Encrypt the document with AES-256. This applies the permissions.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF. Save is called inside the using block.
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with permissions to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}