using System;
using System.IO;                     // For file existence check
using System.Drawing;                // For System.Drawing.Rectangle used by PdfContentEditor
using Aspose.Pdf;                     // Needed to create a placeholder PDF
using Aspose.Pdf.Facades;             // Facade API for editing PDF annotations

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // Source PDF
        const string outputPath = "tooltip_output.pdf"; // Destination PDF

        // ---------------------------------------------------------------------
        // Ensure that a source PDF exists. If it does not, create a minimal PDF
        // with a single blank page. This removes the FileNotFoundException that
        // occurs when the example is run in a fresh environment.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add();               // Add an empty page
                doc.Save(inputPath);            // Save the placeholder PDF
            }
        }

        // Initialize the content editor and bind the source PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (position and size) in points.
            // PdfContentEditor.CreateText expects a System.Drawing.Rectangle
            // where the constructor parameters are (x, y, width, height).
            // We want a 20×20 box at (100,500).
            System.Drawing.Rectangle annotationRect = new System.Drawing.Rectangle(100, 500, 20, 20);

            // Create a text (sticky‑note) annotation.
            // Parameters:
            //   rect    – annotation rectangle (System.Drawing.Rectangle)
            //   title   – title shown in the annotation window (author label)
            //   contents– tooltip text displayed when the mouse hovers over the note
            //   open    – false so the popup is not shown immediately
            //   icon    – visual icon for the note ("Note" is a standard icon)
            //   page    – 1‑based page number where the annotation is placed
            editor.CreateText(
                annotationRect,
                "Info",                                 // Title
                "This is a tooltip shown on hover.",    // Tooltip / contents
                false,                                   // Do not open by default
                "Note",                                 // Icon type
                1);                                      // Page number (1‑based)

            // Save the modified PDF
            editor.Save(outputPath);
        }
    }
}
