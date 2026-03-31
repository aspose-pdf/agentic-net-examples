using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);

        bool hasUsageRights = pdfSignature.ContainsUsageRights();

        Console.WriteLine(hasUsageRights
            ? "The PDF contains extended usage rights."
            : "The PDF does NOT contain extended usage rights.");

        pdfSignature.Close();
    }
}
