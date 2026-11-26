using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "noPwdForbidAll.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF (no password required)
        using (Document doc = new Document(inputPath))
        {
            // No permissions – forbid all operations
            Permissions perms = (Permissions)0; // equivalent to Permissions.None in newer versions

            // Encrypt with AES 128‑bit algorithm; empty strings for user/owner passwords
            doc.Encrypt(string.Empty, string.Empty, perms, CryptoAlgorithm.AESx128);

            // Save the encrypted PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved as '{outputPath}'.");
    }
}
