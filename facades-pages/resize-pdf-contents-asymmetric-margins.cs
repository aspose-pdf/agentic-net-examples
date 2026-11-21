using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the high‑level API.
        Document pdfDoc = new Document(inputPath);

        // Prepare asymmetric margin values (points) using the factory method.
        var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(
            left: 50,
            right: 20,
            top: 0,
            bottom: 0);
        // The ContentsResizeParameters class does not expose a PreserveAspectRatio property.
        // Aspect ratio preservation is the default behaviour, so no explicit setting is required.

        // Build an array with all page numbers (1‑based).
        int pageCount = pdfDoc.Pages.Count;
        int[] allPages = new int[pageCount];
        for (int i = 0; i < pageCount; i++)
            allPages[i] = i + 1;

        // Use PdfFileEditor to apply the resizing.
        PdfFileEditor editor = new PdfFileEditor();
        editor.ResizeContents(pdfDoc, allPages, resizeParams);

        // Save the modified PDF.
        pdfDoc.Save(outputPath);

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}