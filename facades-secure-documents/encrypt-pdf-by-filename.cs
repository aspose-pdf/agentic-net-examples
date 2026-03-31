using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputDirectory = "input";
        string outputDirectory = "output";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Input directory '{inputDirectory}' does not exist.");
            return;
        }

        Directory.CreateDirectory(outputDirectory);

        string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");
        foreach (string inputPath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(outputDirectory, fileName + ".pdf");

            CryptoAlgorithm algorithm = DetermineAlgorithm(fileName);
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;

            using (Document doc = new Document(inputPath))
            {
                doc.Encrypt("user123", "owner123", permissions, algorithm);
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted '{fileName}.pdf' with {algorithm}.");
        }
    }

    private static CryptoAlgorithm DetermineAlgorithm(string fileName)
    {
        if (fileName.IndexOf("rc4", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return CryptoAlgorithm.RC4x128;
        }
        if (fileName.IndexOf("aes256", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return CryptoAlgorithm.AESx256;
        }
        // Default to AES-256 for compliance
        return CryptoAlgorithm.AESx256;
    }
}