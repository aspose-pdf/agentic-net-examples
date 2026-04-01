using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        PdfFileSignature pdfSignature = new PdfFileSignature();
        pdfSignature.BindPdf(inputPath);
        pdfSignature.RemoveSignatures();
        pdfSignature.Save(outputPath);
        Console.WriteLine($"All signatures removed. Saved to '{outputPath}'.");
    }
}