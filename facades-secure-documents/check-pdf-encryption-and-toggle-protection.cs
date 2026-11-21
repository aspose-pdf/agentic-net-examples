using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine whether the PDF is already encrypted.
        var fileInfo = new PdfFileInfo(inputPath);
        bool isEncrypted = fileInfo.IsEncrypted;

        if (isEncrypted)
        {
            // The PDF is encrypted – decrypt it using the owner password.
            using var security = new PdfFileSecurity();
            security.BindPdf(inputPath);
            security.DecryptFile(ownerPassword);
            security.Save(decryptedPath);
            Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
        }
        else
        {
            // The PDF is not encrypted – encrypt it using AES‑256.
            var doc = new Document(inputPath);

            // Set the desired permissions (print & extract content).
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

            // Encrypt with user/owner passwords, the selected permissions,
            // 256‑bit key size and AES algorithm.
            doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);
            doc.Save(encryptedPath);
            Console.WriteLine($"Encrypted PDF saved to '{encryptedPath}'.");
        }
    }
}