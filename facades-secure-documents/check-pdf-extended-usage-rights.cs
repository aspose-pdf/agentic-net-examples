using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Ensure the PDF file exists before processing
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfFileSignature implements IDisposable via SaveableFacade, so use a using block
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the PDF file to the facade for analysis
            pdfSignature.BindPdf(inputPath);

            // Check whether the PDF contains extended usage rights
            bool hasUsageRights = pdfSignature.ContainsUsageRights();

            Console.WriteLine($"Contains usage rights: {hasUsageRights}");
        }
    }
}