using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputDirectory = "input";
        Directory.CreateDirectory(inputDirectory);

        foreach (string filePath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            string fileName = Path.GetFileName(filePath);
            CryptoAlgorithm algorithm = DetermineAlgorithm(fileName);
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContent;
            string userPassword = "user123";
            string ownerPassword = "owner123";

            using (Document document = new Document(filePath))
            {
                document.Encrypt(userPassword, ownerPassword, permissions, algorithm);
                string outputFileName = Path.GetFileNameWithoutExtension(fileName) + "_enc.pdf";
                document.Save(outputFileName);
                Console.WriteLine($"Encrypted {fileName} using {algorithm} -> {outputFileName}");
            }
        }
    }

    private static CryptoAlgorithm DetermineAlgorithm(string fileName)
    {
        string lower = fileName.ToLowerInvariant();
        if (lower.Contains("high"))
        {
            return CryptoAlgorithm.AESx256;
        }
        else if (lower.Contains("medium"))
        {
            return CryptoAlgorithm.AESx128;
        }
        else
        {
            return CryptoAlgorithm.RC4x128;
        }
    }
}