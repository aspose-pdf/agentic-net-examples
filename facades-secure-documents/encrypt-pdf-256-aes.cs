using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document doc = new Document(inputPath);

        // Define desired permissions (e.g., allow printing and content extraction).
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        // Encrypt using 256‑bit AES.
        doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

        // Save the encrypted PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}
