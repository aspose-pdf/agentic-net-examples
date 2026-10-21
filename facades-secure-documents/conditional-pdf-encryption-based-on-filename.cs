using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    // Determine the CryptoAlgorithm based on file name conventions
    static CryptoAlgorithm GetAlgorithm(string fileName)
    {
        // Example conventions:
        //   * contains "high"   -> AESx256 (strongest)
        //   * contains "medium" -> AESx128
        //   * otherwise         -> RC4x128
        string lower = fileName.ToLowerInvariant();
        if (lower.Contains("high"))
            return CryptoAlgorithm.AESx256;
        if (lower.Contains("medium"))
            return CryptoAlgorithm.AESx128;
        return CryptoAlgorithm.RC4x128;
    }

    static void Main()
    {
        const string inputFolder   = @"C:\PdfInput";
        const string outputFolder  = @"C:\PdfEncrypted";
        const string userPassword  = "UserPass123";
        const string ownerPassword = "OwnerPass123";

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        // Permissions to apply to all encrypted PDFs
        Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

        foreach (string inputPath in Directory.GetFiles(inputFolder, "*.pdf"))
        {
            try
            {
                string fileName   = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(outputFolder, $"{fileName}_encrypted.pdf");

                CryptoAlgorithm algorithm = GetAlgorithm(fileName);

                // Load the PDF document
                Document doc = new Document(inputPath);

                // Apply encryption with the selected algorithm
                doc.Encrypt(userPassword, ownerPassword, perms, algorithm);

                // Save the encrypted PDF
                doc.Save(outputPath);

                Console.WriteLine($"Encrypted '{inputPath}' with {algorithm} → '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{inputPath}': {ex.Message}");
            }
        }
    }
}