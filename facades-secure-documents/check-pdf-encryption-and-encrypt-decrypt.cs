using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string encryptedPath  = "encrypted.pdf";
        const string decryptedPath  = "decrypted.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load meta‑information to check encryption status
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        bool isEncrypted = fileInfo.IsEncrypted;
        Console.WriteLine($"IsEncrypted: {isEncrypted}");

        if (!isEncrypted)
        {
            // Encrypt the PDF using the modern Document API
            Document doc = new Document(inputPath);

            // Grant a typical set of permissions (only members that exist in the current Aspose.Pdf version)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
            Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
        }
        else
        {
            // Decrypt the PDF using the modern Document API
            // Open the encrypted PDF with the owner password
            Document doc = new Document(inputPath, ownerPassword);
            doc.Decrypt();
            doc.Save(decryptedPath);
            Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
        }
    }
}
