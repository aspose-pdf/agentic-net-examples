using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security; // Permissions enum resides here in newer versions

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "userWownerW.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Enable only the permission that allows screen readers (extract content with disabilities)
                Permissions perms = Permissions.ExtractContentWithDisabilities;

                // Encrypt with RC4 128‑bit algorithm using the specified passwords
                doc.Encrypt("userW", "ownerW", perms, CryptoAlgorithm.RC4x128);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
