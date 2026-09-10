using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "tooltip_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor implements IDisposable, so wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Define the annotation rectangle (x, y, width, height) in points.
            // Use System.Drawing.Rectangle as required by the API.
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 500, 20, 20);

            // Title appears in the annotation window title bar (optional).
            string title = "Info";

            // Contents is the tooltip text shown when the mouse hovers over the annotation.
            string contents = "Additional information displayed as a tooltip.";

            // Open = false ensures the annotation is not displayed open by default;
            // the tooltip appears on hover.
            bool open = false;

            // Choose an icon style; "Note" is a common choice.
            string icon = "Note";

            // Page numbers are 1‑based in Aspose.Pdf.
            int page = 1;

            // Create the text (sticky‑note) annotation which acts as a tooltip.
            editor.CreateText(rect, title, contents, open, icon, page);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Tooltip annotation added. Saved to '{outputPath}'.");
    }
}