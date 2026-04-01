using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Encrypt the PDF
        PdfFileSecurity encryptor = new PdfFileSecurity();
        encryptor.BindPdf(inputPath);
        encryptor.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);
        encryptor.Save(encryptedPath);
        encryptor.Close();

        // Decrypt the PDF
        PdfFileSecurity decryptor = new PdfFileSecurity();
        decryptor.BindPdf(encryptedPath);
        decryptor.DecryptFile(ownerPassword);
        decryptor.Save(decryptedPath);
        decryptor.Close();

        // Verify round‑trip integrity by comparing page counts
        using (Document original = new Document(inputPath))
        using (Document decrypted = new Document(decryptedPath))
        {
            bool samePageCount = original.Pages.Count == decrypted.Pages.Count;
            Console.WriteLine(samePageCount ? "PASS: Page count matches" : "FAIL: Page count differs");
        }
    }
}