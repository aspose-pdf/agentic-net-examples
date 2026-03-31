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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        PdfFileInfo pdfInfo = new PdfFileInfo();
        pdfInfo.BindPdf(inputPath);
        string utcNow = DateTime.UtcNow.ToString("o");
        pdfInfo.SetMetaInfo("LastUpdated", utcNow);
        bool success = pdfInfo.SaveNewInfo(outputPath);
        if (success)
        {
            Console.WriteLine("Custom metadata added. Saved to " + outputPath);
        }
        else
        {
            Console.Error.WriteLine("Failed to save updated PDF.");
        }
    }
}