using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string folderPath = "secure";
        string userPassword = "myPassword";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
        foreach (string inputPath in pdfFiles)
        {
            try
            {
                using (Document doc = new Document(inputPath))
                {
                    Permissions permissions = Permissions.PrintDocument;
                    doc.Encrypt(userPassword, null, permissions, CryptoAlgorithm.AESx256);
                    doc.Save(inputPath);
                }
                Console.WriteLine($"Protected: {Path.GetFileName(inputPath)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing {Path.GetFileName(inputPath)}: {ex.Message}");
            }
        }
    }
}