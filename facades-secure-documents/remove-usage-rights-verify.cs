using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_no_usage_rights.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfFileSignature facade to manipulate usage rights
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the source PDF
            pdfSign.BindPdf(inputPath);

            // Remove any existing usage rights
            pdfSign.RemoveUsageRights();

            // Verify removal by checking if usage rights still exist
            bool hasUsageRights = pdfSign.ContainsUsageRights();
            Console.WriteLine($"Contains usage rights after removal: {hasUsageRights}");

            // Save the modified PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}