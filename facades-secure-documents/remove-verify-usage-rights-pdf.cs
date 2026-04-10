using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF, remove usage rights, verify removal, and save.
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Load the PDF file into the facade.
            pdfSign.BindPdf(inputPath);

            // Remove the usage rights entry from the document.
            pdfSign.RemoveUsageRights();

            // Check whether usage rights are still present.
            bool hasUsageRights = pdfSign.ContainsUsageRights();
            Console.WriteLine($"Contains usage rights after removal: {hasUsageRights}");

            // Save the modified PDF to a new file.
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}