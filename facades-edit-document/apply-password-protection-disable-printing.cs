using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "protected.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF
        Document doc = new Document(inputPath);

        // Disable all permissions (printing, copying, etc.) by using a zero‑value flag
        Permissions permissions = (Permissions)0; // equivalent to "no permissions"

        // Apply password protection: user password "user123", owner password "admin456"
        doc.Encrypt("user123", "admin456", permissions, CryptoAlgorithm.AESx256);

        // Save the protected PDF
        doc.Save(outputPath);

        Console.WriteLine($"Password protection applied. Output saved to '{outputPath}'.");
    }
}
