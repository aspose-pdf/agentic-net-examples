using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_no_usagerights.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);
            pdfSign.RemoveUsageRights();

            bool hasUsageRights = pdfSign.ContainsUsageRights();
            Console.WriteLine($"Contains usage rights after removal: {hasUsageRights}");

            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Saved PDF without usage rights to '{outputPath}'.");
    }
}