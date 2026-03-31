using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfFileInfo pdfInfo = new PdfFileInfo();
        pdfInfo.BindPdf(inputPath);
        pdfInfo.SetMetaInfo("Version", "1.0");
        bool success = pdfInfo.SaveNewInfoWithXmp(outputPath);
        Console.WriteLine(success ? $"Metadata updated and saved to '{outputPath}'." : "Failed to save PDF.");
    }
}
