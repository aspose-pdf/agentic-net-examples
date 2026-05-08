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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, remove usage rights, verify removal, and save.
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);          // Load PDF into the facade.
            pdfSignature.RemoveUsageRights();         // Remove the usage rights entry.

            // Verify that usage rights have been removed.
            bool hasUsageRights = pdfSignature.ContainsUsageRights();
            Console.WriteLine($"Contains usage rights after removal: {hasUsageRights}");

            // Save the modified PDF.
            pdfSignature.Save(outputPath);
        }

        Console.WriteLine($"PDF saved without usage rights to '{outputPath}'.");
    }
}