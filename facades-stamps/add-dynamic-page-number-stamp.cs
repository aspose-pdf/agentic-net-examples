using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the facade with input and output file paths
        // (PdfFileStamp does NOT implement IDisposable, so no using block)
        PdfFileStamp fileStamp = new PdfFileStamp(inputPdf, outputPdf);

        // Desired format using a custom placeholder {page_number}
        string customFormat = "Page {page_number}";

        // Aspose.Pdf expects '#' as the page‑number placeholder.
        // Replace the custom placeholder with the required one.
        string aspFormat = customFormat.Replace("{page_number}", "#");

        // Add the page‑number stamp to all pages.
        fileStamp.AddPageNumber(aspFormat);

        // Close the facade to write the output file.
        fileStamp.Close();

        Console.WriteLine($"Page numbers added and saved to '{outputPdf}'.");
    }
}