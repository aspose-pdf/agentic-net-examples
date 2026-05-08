using System;
using System.IO;
using System.Drawing;                     // needed for Rectangle and Color
using Aspose.Pdf.Facades;                // PdfContentEditor facade

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // PDF with added hyperlink
        const int  sourcePage = 1;                 // page where the link rectangle will be placed (1‑based)
        const int  targetPage = 3;                 // page to navigate to when the link is clicked (1‑based)

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Create the editor, bind the PDF, add the local link, and save the result
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document into the facade
            editor.BindPdf(inputPdf);

            // Define the clickable rectangle (coordinates are in points, origin at bottom‑left)
            // Example: rectangle at (100, 500) with width 200 and height 50
            Rectangle linkRect = new Rectangle(100, 500, 300, 550);

            // Create a local link that jumps from sourcePage to targetPage
            // The overload without color uses the default link appearance
            editor.CreateLocalLink(linkRect, targetPage, sourcePage, Color.Blue);

            // Persist the changes to a new file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Hyperlink annotation added. Output saved to '{outputPdf}'.");
    }
}