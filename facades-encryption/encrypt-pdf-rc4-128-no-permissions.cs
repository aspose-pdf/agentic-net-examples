using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF.
        Document pdfDocument = new Document(inputPath);

        // Forbid all privileges (no permissions) and encrypt using 128‑bit RC4.
        // Use a cast to zero because the Permissions enum does not define a "None" member.
        Permissions noPermissions = (Permissions)0;
        pdfDocument.Encrypt(userPassword, ownerPassword, noPermissions, CryptoAlgorithm.RC4x128);

        // Save the encrypted PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}
