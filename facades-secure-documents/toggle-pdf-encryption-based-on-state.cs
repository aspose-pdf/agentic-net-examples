using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Check whether the PDF is encrypted
        bool isEncrypted;
        using (PdfFileInfo info = new PdfFileInfo(inputPath))
        {
            isEncrypted = info.IsEncrypted;
        }

        if (isEncrypted)
        {
            // Decrypt the encrypted PDF
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                security.BindPdf(inputPath);                     // Load the PDF
                security.DecryptFile(ownerPassword);            // Decrypt using owner password
                security.Save(outputPath);                      // Save the decrypted PDF
            }
            Console.WriteLine("PDF was encrypted and has been decrypted.");
        }
        else
        {
            // Encrypt the unencrypted PDF
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                security.BindPdf(inputPath);                     // Load the PDF
                // Define privileges (example: allow all actions)
                DocumentPrivilege privilege = DocumentPrivilege.AllowAll;
                // Encrypt with AES-256 algorithm
                security.EncryptFile(userPassword, ownerPassword, privilege, KeySize.x256);
                security.Save(outputPath);                      // Save the encrypted PDF
            }
            Console.WriteLine("PDF was not encrypted and has been encrypted.");
        }
    }
}