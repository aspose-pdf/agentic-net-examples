using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("File not found: " + inputPath);
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);
            bool hasUsageRights = pdfSignature.ContainsUsageRights();
            Console.WriteLine(hasUsageRights ? "PDF contains extended usage rights." : "PDF does not contain extended usage rights.");
        }
    }
}