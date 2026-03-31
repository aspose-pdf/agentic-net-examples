using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            bool isEncrypted = doc.IsEncrypted;
            Console.WriteLine($"IsEncrypted: {isEncrypted}");

            if (!isEncrypted)
            {
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
                doc.Save(outputPath);
                Console.WriteLine($"Document encrypted and saved to '{outputPath}'.");
            }
            else
            {
                Console.WriteLine("Document is already encrypted. No action taken.");
            }
        }
    }
}