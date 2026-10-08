using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";   // PDF that already contains the PNG
        const string gifPath = "overlay.gif"; // Semi‑transparent GIF to overlay
        const string outputPath = "output.pdf";
        const int    pageNumber = 1;            // 1‑based page index

        // Validate input files
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(gifPath))
        {
            Console.Error.WriteLine($"GIF not found: {gifPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(pdfPath);

        // Create an ImageStamp for the GIF
        ImageStamp gifStamp = new ImageStamp(gifPath)
        {
            // Draw over existing page content
            Background = false,
            // Position – XIndent/YIndent are measured from the lower‑left corner (points)
            XIndent = 100f,
            YIndent = 200f,
            // Set semi‑transparent opacity directly on the stamp
            Opacity = 0.5f // 50 % opacity
        };

        // Add the stamp to the required page (pages are 1‑based)
        pdfDocument.Pages[pageNumber].AddStamp(gifStamp);

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Overlay applied successfully. Saved to '{outputPath}'.");
    }
}
