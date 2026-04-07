using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Allow copying text (ExtractContent) and disallow form editing and annotation changes
            Permissions permissions = Permissions.ExtractContent;
            doc.Encrypt(string.Empty, string.Empty, permissions, CryptoAlgorithm.AESx256);
            doc.Save(outputPath);
        }

        Console.WriteLine($"Permissions applied and saved to '{outputPath}'.");
    }
}