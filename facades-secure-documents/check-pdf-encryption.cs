using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string ownerPassword = "ownerpass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Check encryption status using PdfFileInfo
        Aspose.Pdf.Facades.PdfFileInfo fileInfo = new Aspose.Pdf.Facades.PdfFileInfo(inputPath);
        bool isEncrypted = fileInfo.IsEncrypted;
        Console.WriteLine($"IsEncrypted: {isEncrypted}");

        if (isEncrypted)
        {
            // Decrypt the PDF using PdfFileSecurity
            using (PdfFileSecurity security = new PdfFileSecurity())
            {
                security.BindPdf(inputPath);
                security.DecryptFile(ownerPassword);
                security.Save(outputPath);
            }
            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
        }
        else
        {
            // If not encrypted, simply copy the file
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine($"File is not encrypted. Copied to '{outputPath}'.");
        }
    }
}