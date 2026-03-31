using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);
            bool hasUsageRights = pdfSignature.ContainsUsageRights();
            Console.WriteLine($"Contains usage rights: {hasUsageRights}");
            pdfSignature.RemoveUsageRights();
            pdfSignature.Save(outputPath);
        }

        Console.WriteLine($"Usage rights removed and saved to '{outputPath}'.");
    }
}
