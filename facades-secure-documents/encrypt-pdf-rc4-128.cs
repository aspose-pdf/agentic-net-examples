using System;
using System.IO;
using Aspose.Pdf;

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

        using (Document doc = new Document(inputPath))
        {
            // Combine privileges: allow printing and editing (modify content)
            Aspose.Pdf.Permissions permissions = Aspose.Pdf.Permissions.PrintDocument | Aspose.Pdf.Permissions.ModifyContent;

            // Encrypt with RC4 128‑bit algorithm
            doc.Encrypt(userPassword, ownerPassword, permissions, Aspose.Pdf.CryptoAlgorithm.RC4x128);

            doc.Save(outputPath);
        }

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}