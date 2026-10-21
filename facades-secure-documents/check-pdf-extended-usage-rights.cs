using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the file exists before processing
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF before checking usage rights
        PdfFileSignature signature = new PdfFileSignature();
        signature.BindPdf(inputPath);

        // Detect extended usage rights in the PDF
        bool hasUsageRights = signature.ContainsUsageRights();

        Console.WriteLine($"Extended usage rights present: {hasUsageRights}");
    }
}