using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        fileSecurity.BindPdf(inputPath);
        // Encrypt with 256‑bit AES, allowing only printing (example privilege)
        fileSecurity.EncryptFile(userPassword, ownerPassword, DocumentPrivilege.Print, KeySize.x256);
        fileSecurity.Save(outputPath);
        Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
    }
}