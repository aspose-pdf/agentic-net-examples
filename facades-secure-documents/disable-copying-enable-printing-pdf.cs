using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_protected.pdf";
        const string userPassword = "";          // allow opening without password
        const string ownerPassword = "owner123"; // owner password to control permissions

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Enable printing, disable copying (content extraction)
        Permissions permissions = Permissions.PrintDocument;

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Encrypt using AES‑256 with the specified permissions
        doc.Encrypt(userPassword, ownerPassword, permissions, CryptoAlgorithm.AESx256);

        // Save the protected PDF
        doc.Save(outputPath);

        Console.WriteLine($"Protected PDF saved to '{outputPath}' (printing enabled, copying disabled).");
    }
}
