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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfFileSignature facade and bind the PDF file
        using (PdfFileSignature signature = new PdfFileSignature())
        {
            signature.BindPdf(inputPath);

            // Detect whether the PDF contains extended usage rights
            bool hasUsageRights = signature.ContainsUsageRights();

            Console.WriteLine($"Extended usage rights present: {hasUsageRights}");
        }
    }
}