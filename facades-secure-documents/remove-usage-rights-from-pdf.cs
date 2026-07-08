using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPath))
        {
            var doc = new Document();
            doc.Pages.Add(); // add a blank page
            doc.Save(inputPath);
            Console.WriteLine($"Sample PDF created at '{inputPath}'.");
        }

        // Load the PDF, remove usage rights, verify removal, and save the result.
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);          // Load PDF into the facade.
            pdfSignature.RemoveUsageRights();         // Remove usage rights entry.

            // Verify that usage rights are no longer present.
            bool hasUsageRights = pdfSignature.ContainsUsageRights();
            Console.WriteLine($"Contains usage rights after removal: {hasUsageRights}");

            // Save the modified PDF.
            pdfSignature.Save(outputPath);
        }
    }
}
