using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "decrypted.pdf";
        const string ownerPassword = "ownerpass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Verify that the PDF is encrypted before attempting decryption
        var fileInfo = new PdfFileInfo(inputPath);
        if (!fileInfo.IsEncrypted)
        {
            Console.WriteLine("PDF is not encrypted; copying original file.");
            File.Copy(inputPath, outputPath, true);
            return;
        }

        try
        {
            // Use PdfFileSecurity to decrypt the PDF with the owner password
            using var security = new PdfFileSecurity();
            security.BindPdf(inputPath);
            security.DecryptFile(ownerPassword);
            security.Save(outputPath);

            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Decryption failed: {ex.Message}");
        }
    }
}
