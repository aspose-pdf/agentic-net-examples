using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "secured.pdf";
        const string userPassword = "user123";
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
                // Define permissions: allow printing and content modification,
                // but do NOT include ExtractContent (copy‑paste) permission.
                Permissions perms = Permissions.PrintDocument | Permissions.ModifyContent;

                // Apply encryption with AES‑256 algorithm.
                // This creates a security handler that enforces the specified permissions.
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Secured PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
