using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfPageEditor does NOT implement IDisposable, so no using block is needed
        PdfPageEditor pageEditor = new PdfPageEditor();
        pageEditor.BindPdf(inputPath);

        // Retrieve total page count via the underlying Document object
        int pageCount = pageEditor.Document?.Pages?.Count ?? 0;
        Console.WriteLine($"Loaded PDF with {pageCount} pages.");

        // Optional preparation: rotate the first page 90 degrees clockwise
        if (pageCount > 0)
        {
            // Rotation enum values: on0, on90, on180, on270
            pageEditor.Document.Pages[1].Rotate = Rotation.on90;
        }

        // Save the (potentially modified) PDF to a new file
        const string outputPath = "edited.pdf";
        pageEditor.Save(outputPath);
        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}
