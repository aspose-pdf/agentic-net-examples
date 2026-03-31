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

        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);
        pdfSignature.RemoveUsageRights();

        bool hasUsageRights = pdfSignature.ContainsUsageRights();
        Console.WriteLine("Contains usage rights after removal: " + hasUsageRights);

        pdfSignature.Save(outputPath);
        Console.WriteLine("Processed PDF saved to '" + outputPath + "'.");
    }
}