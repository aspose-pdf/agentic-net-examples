using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string zoomedPath  = "zoomed_page4.pdf";
        const string outputPath  = "final_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Apply a 200% zoom to page 4 using PdfPageEditor
        // ------------------------------------------------------------
        using (PdfPageEditor editor = new PdfPageEditor())
        {
            editor.BindPdf(inputPath);               // Load source PDF
            editor.ProcessPages = new int[] { 4 };   // Target page (1‑based index)
            editor.Zoom = 2.0f;                      // 200% magnification
            editor.Save(zoomedPath);                 // Save intermediate PDF
        }

        // ------------------------------------------------------------
        // Step 2: Add a text stamp at point (1,2) on the same page
        // ------------------------------------------------------------
        using (Document doc = new Document(zoomedPath))
        {
            // Create a text stamp with desired content
            TextStamp stamp = new TextStamp("Enhanced Readability");

            // Horizontal alignment (centered relative to the stamp's origin)
            stamp.HorizontalAlignment = HorizontalAlignment.Center;

            // Position the stamp using XIndent/YIndent (points from the left/bottom of the page)
            stamp.XIndent = 1;   // X coordinate (point 1)
            stamp.YIndent = 2;   // Y coordinate (point 2)

            // Add the stamp to page 4 (pages are 1‑based)
            doc.Pages[4].AddStamp(stamp);

            // Save the final PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processing complete. Output saved to '{outputPath}'.");
    }
}
