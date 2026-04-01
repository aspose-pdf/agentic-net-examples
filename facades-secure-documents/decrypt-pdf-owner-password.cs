using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "decrypted.pdf";
        const string ownerPassword = "ownerPwd";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileSecurity fileSecurity = new PdfFileSecurity();
        fileSecurity.BindPdf(inputPath);
        bool success = fileSecurity.DecryptFile(ownerPassword);
        if (!success)
        {
            Console.Error.WriteLine("Decryption failed.");
            return;
        }
        fileSecurity.Save(outputPath);
        Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
    }
}