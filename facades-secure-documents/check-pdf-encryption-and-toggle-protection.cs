using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // <-- required for PdfFileInfo

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

        // Load PDF meta‑information to check encryption status
        using (PdfFileInfo fileInfo = new PdfFileInfo(inputPath))
        {
            bool isEncrypted = fileInfo.IsEncrypted;
            Console.WriteLine($"IsEncrypted: {isEncrypted}");

            if (!isEncrypted)
            {
                // PDF is not encrypted – encrypt it using Document.Encrypt with Permissions
                Document doc = new Document(inputPath);
                // Allow printing only; combine other permissions with bitwise OR if needed
                Permissions perms = Permissions.PrintDocument;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx128);
                doc.Save(outputPath);
            }
            else
            {
                // PDF is already encrypted – decrypt it by opening with the owner password and saving without encryption
                Document doc = new Document(inputPath, ownerPassword);
                // Saving without calling Encrypt removes protection
                doc.Save(outputPath);
            }
        }
    }
}
