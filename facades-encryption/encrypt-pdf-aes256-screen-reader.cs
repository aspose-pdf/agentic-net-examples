using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // optional, ensures Permissions enum is resolved

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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Enable screen‑reader permission (ExtractContentWithDisabilities)
        Permissions permissions = Permissions.ExtractContentWithDisabilities;

        // Apply 256‑bit AES encryption with the specified passwords and permission
        doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

        // Save the encrypted PDF
        doc.Save(outputPath);

        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}