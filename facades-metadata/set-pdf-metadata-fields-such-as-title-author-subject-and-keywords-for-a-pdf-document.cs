using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for PDF metadata handling

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_with_metadata.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF via PdfFileInfo facade (lifecycle: create → load)
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            // Set standard metadata properties
            pdfInfo.Title    = "Sample Document Title";
            pdfInfo.Author   = "John Doe";
            pdfInfo.Subject  = "Demonstration of metadata setting";
            pdfInfo.Keywords = "Aspose.Pdf, Metadata, C#";

            // Save the updated PDF (lifecycle: save)
            pdfInfo.SaveNewInfo(outputPdf);
        }

        Console.WriteLine($"Metadata updated and saved to '{outputPdf}'.");
    }
}