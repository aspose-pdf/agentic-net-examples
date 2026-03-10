using System;
using System.IO;
using Aspose.Pdf.Facades;   // Facade classes for PDF operations

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfViewer is a facade that loads a PDF into memory.
        // It implements IDisposable, so we wrap it in a using block.
        using (PdfViewer viewer = new PdfViewer())
        {
            // Load the PDF file. The PDF is now bound to the viewer and ready for further processing.
            viewer.BindPdf(inputPath);

            // At this point the PDF is loaded in memory.
            // Subsequent operations (e.g., printing, extracting pages, etc.) can be performed using the viewer.
            Console.WriteLine($"PDF '{inputPath}' successfully loaded into PdfViewer.");
        }
    }
}