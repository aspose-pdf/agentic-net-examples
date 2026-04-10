using System;
using System.Drawing;                     // System.Drawing.Rectangle
using Aspose.Pdf;                     // Document class
using Aspose.Pdf.Facades;           // PdfContentEditor facade
using Aspose.Pdf.Text;               // TextFragment class

class Program
{
    static void Main()
    {
        // Paths to the source and the resulting PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure a source PDF exists – create a simple three‑page document if it does not.
        if (!System.IO.File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath, 3);
        }

        // Define the rectangle (in points) where the clickable area will appear
        // (x, y) = lower‑left corner, width and height specify the size
        // System.Drawing.Rectangle expects (x, y, width, height)
        System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 500, 200, 50);

        // Page on which the link rectangle will be placed (1‑based)
        int originalPage = 1;

        // Destination page number to jump to when the link is activated (1‑based)
        int destinationPage = 3;

        // Create and configure the content editor
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Create a local link that points to another page in the same document
            // Overload without color uses the default link appearance
            editor.CreateLocalLink(linkRect, destinationPage, originalPage);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Hyperlink annotation added. Output saved to '{outputPath}'.");
    }

    /// <summary>
    /// Creates a minimal PDF with the specified number of blank pages.
    /// Each page contains a simple text paragraph so the file is not completely empty.
    /// </summary>
    private static void CreateSamplePdf(string path, int pageCount)
    {
        // Create a new document
        Document doc = new Document();

        for (int i = 1; i <= pageCount; i++)
        {
            // Add a new page
            Page page = doc.Pages.Add();

            // Add a paragraph that identifies the page number
            page.Paragraphs.Add(new TextFragment($"Page {i}"));
        }

        // Save the document to the supplied path
        doc.Save(path);
    }
}
