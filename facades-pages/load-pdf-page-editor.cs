using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "sample.pdf";
        const string outputPath = "edited.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize PdfPageEditor and bind the PDF file for editing
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPath);

            // Optional editing operations can be performed here, e.g., delete a page:
            // pageEditor.DeletePage(1);

            // Save the (potentially edited) PDF document
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF processed and saved to '{outputPath}'.");
    }
}
