using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";
        const string ownerPassword = "ownerPwd";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        // Verify whether the PDF is actually encrypted
        var fileInfo = new PdfFileInfo(encryptedPath);
        if (!fileInfo.IsEncrypted)
        {
            Console.WriteLine("PDF is not encrypted – copying original file.");
            File.Copy(encryptedPath, decryptedPath, true);
            return;
        }

        // Decrypt the PDF using the owner password
        using var security = new PdfFileSecurity();
        security.BindPdf(encryptedPath);
        security.DecryptFile(ownerPassword);
        security.Save(decryptedPath);

        Console.WriteLine($"Decrypted PDF saved to '{decryptedPath}'.");
    }
}