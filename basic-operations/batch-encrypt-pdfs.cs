using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputDir = "input-pdfs";
        const string outputDir = "encrypted-pdfs";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!Directory.Exists(inputDir))
        {
            Console.Error.WriteLine($"Input directory not found: {inputDir}");
            return;
        }
        Directory.CreateDirectory(outputDir);

        string[] pdfFiles = Directory.GetFiles(inputDir, "*.pdf");
        foreach (string filePath in pdfFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string encryptedPath = Path.Combine(outputDir, fileName + "_enc.pdf");

            try
            {
                using (Document doc = new Document(filePath))
                {
                    Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                    doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                    doc.Save(encryptedPath);
                }
                Console.WriteLine($"Encrypted: {encryptedPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt {filePath}: {ex.Message}");
            }
        }
    }
}