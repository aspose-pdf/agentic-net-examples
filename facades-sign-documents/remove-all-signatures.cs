using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "signed.pdf";
        const string outputPath = "clean.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);
            pdfSignature.RemoveSignatures();
            pdfSignature.Save(outputPath);
        }

        Console.WriteLine($"All signatures removed. Clean file saved as '{outputPath}'.");
    }
}