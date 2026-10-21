using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";
        const string userPassword = "";          // No user password (openable without password)
        const string ownerPassword = "owner123"; // Owner password to control permissions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        Document doc = new Document(inputPath);

        // Enable printing, disable copying (do NOT include ExtractContent permission)
        Permissions permissions = Permissions.PrintDocument;

        // Encrypt using AES‑256 algorithm
        doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

        // Save the protected PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved with printing enabled and copying disabled: {outputPath}");
    }
}
