using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string samplePath = "sample.pdf";
        const string encryptedPath = "encrypted.pdf";

        // Create a simple PDF with one blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            doc.Save(samplePath);
        }

        // Encrypt the PDF with known passwords
        PdfFileSecurity encryptor = new PdfFileSecurity();
        encryptor.BindPdf(samplePath);
        encryptor.EncryptFile("user123", "owner123", DocumentPrivilege.Print, KeySize.x256, Algorithm.AES);
        encryptor.Save(encryptedPath);

        // Attempt to decrypt with an incorrect owner password
        PdfFileSecurity decryptor = new PdfFileSecurity();
        decryptor.BindPdf(encryptedPath);
        bool success = decryptor.TryDecryptFile("wrongowner");

        if (success)
        {
            Console.WriteLine("Decryption unexpectedly succeeded with wrong password.");
        }
        else
        {
            Console.WriteLine("Decryption failed as expected with incorrect owner password.");
        }
    }
}