using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create a new PdfXmpMetadata instance
        PdfXmpMetadata xmpMetadata = new PdfXmpMetadata();

        // Bind the existing PDF file to the XMP metadata object
        xmpMetadata.BindPdf(inputPdf);

        // Example: set a title in the XMP metadata (optional)
        // xmpMetadata.SetTitle("Sample PDF Title");

        // Save the PDF with the bound (and possibly modified) XMP metadata
        xmpMetadata.Save(outputPdf);

        Console.WriteLine($"PDF with XMP metadata saved to '{outputPdf}'.");
    }
}