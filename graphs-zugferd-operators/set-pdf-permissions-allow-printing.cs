using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword  = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Allow printing only; do not include ExtractContent or annotation‑modification flags
            Permissions perms = Permissions.PrintDocument;

            // Encrypt with the chosen permissions using a strong algorithm
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

            // Save the protected PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with restricted permissions to '{outputPath}'.");
    }
}