using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "decrypted.pdf";
        const string password   = "ownerpass"; // owner or user password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Check whether the PDF is encrypted
        PdfFileInfo fileInfo = new PdfFileInfo(inputPath);
        bool isEncrypted = fileInfo.IsEncrypted;

        if (!isEncrypted)
        {
            // No encryption – simply copy the file
            File.Copy(inputPath, outputPath, overwrite: true);
            Console.WriteLine("File is not encrypted. Copied without decryption.");
            return;
        }

        // PDF is encrypted – attempt decryption using PdfFileSecurity
        PdfFileSecurity security = new PdfFileSecurity();
        security.BindPdf(inputPath);                     // initialize with source file
        bool decrypted = security.TryDecryptFile(password);

        if (decrypted)
        {
            // Save the decrypted PDF to the output path
            security.Save(outputPath);
            Console.WriteLine($"Decryption succeeded. Saved to '{outputPath}'.");
        }
        else
        {
            Console.Error.WriteLine("Decryption failed. Incorrect password or unsupported encryption.");
        }

        // Release resources held by the facade
        security.Close();
    }
}