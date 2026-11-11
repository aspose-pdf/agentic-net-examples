using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the high‑level Document API (required for resizing)
        Document doc = new Document(inputPath);

        // Define a uniform margin (e.g., 50 points) on all sides
        double margin = 50.0;

        // Retrieve original page dimensions (assumes all pages have same size)
        Page firstPage = doc.Pages[1];
        double pageWidth = firstPage.PageInfo.Width;
        double pageHeight = firstPage.PageInfo.Height;

        // Set up resize parameters using the Margins factory method
        // This creates a ContentsResizeParameters instance with the specified margins
        var resizeParams = PdfFileEditor.ContentsResizeParameters.Margins(margin, margin, margin, margin);

        // Prepare an array with all page numbers (1‑based) – the overload expects 1‑based indices
        int pageCount = doc.Pages.Count;
        int[] pages = new int[pageCount];
        for (int i = 0; i < pageCount; i++)
            pages[i] = i + 1;

        // Use PdfFileEditor to apply the resizing to the selected pages
        PdfFileEditor editor = new PdfFileEditor();
        editor.ResizeContents(doc, pages, resizeParams);

        // Save the resized PDF
        doc.Save(outputPath);
        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}
