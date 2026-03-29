using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string folderPath = "secure";
        const string userPassword = "MySecretPassword";

        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
        foreach (string inputFile in pdfFiles)
        {
            try
            {
                using (Document doc = new Document(inputFile))
                {
                    Permissions perms = Permissions.PrintDocument | Permissions.ModifyContent | Permissions.ExtractContent;
                    doc.Encrypt(userPassword, null, perms, CryptoAlgorithm.AESx256);
                    doc.Save(inputFile);
                }
                Console.WriteLine($"Encrypted: {Path.GetFileName(inputFile)}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to encrypt {Path.GetFileName(inputFile)}: {ex.Message}");
            }
        }
    }
}