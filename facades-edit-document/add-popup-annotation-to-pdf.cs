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
        const string outputPath = "output_popup.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor to edit the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the PDF document
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height)
            // System.Drawing.Rectangle is required by CreatePopup.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 700, 100, 200);

            // Create a popup annotation that appears on hover/click.
            //   rect      – location and size of the popup
            //   "This is a hover note." – text displayed in the popup
            //   false     – do not display the popup open initially (appears on hover/click)
            //   1         – page number (1‑based indexing)
            editor.CreatePopup(rect, "This is a hover note.", false, 1);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Popup annotation added. Saved to '{outputPath}'.");
    }
}
