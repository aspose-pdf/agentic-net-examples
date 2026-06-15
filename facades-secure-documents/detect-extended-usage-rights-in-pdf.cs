using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the PDF file
        using (PdfFileSignature signature = new PdfFileSignature())
        {
            signature.BindPdf(inputPath);

            // Detect if the PDF contains extended usage rights
            bool hasUsageRights = signature.ContainsUsageRights();

            Console.WriteLine($"Extended usage rights present: {hasUsageRights}");
        }
    }
}