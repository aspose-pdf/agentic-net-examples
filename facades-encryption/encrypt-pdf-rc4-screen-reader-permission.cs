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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Define permissions – allow screen readers (ExtractContentWithDisabilities)
        Permissions permissions = Permissions.ExtractContentWithDisabilities;

        // Encrypt the document using 128‑bit RC4 algorithm
        pdfDocument.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.RC4x128);

        // Save the encrypted PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}