using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string password = "userPassword"; // owner password
        const string outputFolder = "Decrypted";
        const string outputFileName = "decrypted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);
        string outputPath = Path.Combine(outputFolder, outputFileName);

        // Check encryption status using PdfFileInfo (recommended API)
        var fileInfo = new PdfFileInfo(inputPath);
        if (!fileInfo.IsEncrypted)
        {
            // Not encrypted – simply copy the file
            File.Copy(inputPath, outputPath, true);
            Console.WriteLine("File was not encrypted; copied to output.");
            return;
        }

        // Decrypt the encrypted PDF using PdfFileSecurity
        try
        {
            using var security = new PdfFileSecurity();
            security.BindPdf(inputPath);               // bind the source PDF
            security.DecryptFile(password);            // decrypt with the owner password
            security.Save(outputPath);                  // save the decrypted PDF

            Console.WriteLine($"Decrypted PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Decryption failed: {ex.Message}");
        }
    }
}
