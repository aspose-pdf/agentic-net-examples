using System;
using System.Drawing;               // Required for Rectangle and Color (used by PdfContentEditor)
using System.IO;
using Aspose.Pdf.Facades;          // Facade classes for PDF annotation editing

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "highlighted_output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPdf}");
            return;
        }

        try
        {
            // Initialize the PdfContentEditor facade
            PdfContentEditor editor = new PdfContentEditor();

            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Define the rectangle (in points) where the highlight will appear.
            // Rectangle(x, y, width, height) – origin is bottom‑left of the page.
            // Adjust these values to match the text you want to highlight.
            Rectangle highlightRect = new Rectangle(100, 500, 200, 20);

            // Create a highlight markup annotation:
            // type = 0  → Highlight
            // page = 1  → First page (Aspose.Pdf uses 1‑based page indexing)
            // color = Yellow (any System.Drawing.Color can be used)
            editor.CreateMarkup(highlightRect,
                                "Sample highlighted text",
                                0,               // 0 = Highlight
                                1,               // page number
                                Color.Yellow);  // highlight color

            // Save the modified PDF to a new file
            editor.Save(outputPdf);

            Console.WriteLine($"Highlight annotation added successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}