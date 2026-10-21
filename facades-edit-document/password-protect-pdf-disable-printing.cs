using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define permissions – allow typical operations but disable printing.
        // Use only enum members that exist in older Aspose.Pdf versions.
        Permissions perms = Permissions.ModifyContent |
                             Permissions.ExtractContent |
                             Permissions.AssembleDocument;
        // Note: Do NOT include Permissions.PrintDocument or Permissions.HighQualityPrint to keep printing disabled.

        // Apply password protection (user password: user123, owner password: admin456)
        doc.Encrypt("user123", "admin456", perms, CryptoAlgorithm.RC4x128);

        // Save the protected PDF
        doc.Save(outputPath);

        Console.WriteLine($"Password protection applied. Output saved to '{outputPath}'.");
    }
}
