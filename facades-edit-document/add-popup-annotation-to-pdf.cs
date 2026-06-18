using System;
using System.IO;
using System.Drawing; // for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and edit it using PdfContentEditor (facade API)
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF file
            editor.BindPdf(inputPath);

            // Define the rectangle where the popup annotation will be placed.
            // System.Drawing.Rectangle expects (x, y, width, height) as integers.
            System.Drawing.Rectangle popupRect = new System.Drawing.Rectangle(100, 700, 100, 100);

            // Create a popup annotation.
            // Parameters: rectangle, contents, open (false = hidden until hover/click), page number (1‑based)
            editor.CreatePopup(popupRect, "This is a helpful note that appears on hover.", false, 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with popup annotation saved to '{outputPath}'.");
    }
}
