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

        // Check encryption status using PdfFileInfo (Facades)
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        bool isEncrypted = fileInfo.IsEncrypted;
        Console.WriteLine($"IsEncrypted: {isEncrypted}");

        if (!isEncrypted)
        {
            // Encrypt the PDF if it is not already encrypted
            Document doc = new Document(inputPath);
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
            Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
        }
        else
        {
            // Decrypt the PDF if it is already encrypted using PdfFileSecurity
            PdfFileSecurity security = new PdfFileSecurity();
            security.BindPdf(inputPath);
            // Decrypt with the user password (or owner password if required)
            security.DecryptFile(userPassword);
            security.Save(decryptedPath);
            Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
        }
    }
}
