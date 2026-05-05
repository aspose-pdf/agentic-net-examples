using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPath  = "input.pdf";
        // Output PDF with the popup annotation
        const string outputPath = "output_with_popup.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfContentEditor facade with the opened document
            PdfContentEditor editor = new PdfContentEditor(doc);

            // Define the rectangle (x, y, width, height) where the popup will be active.
            // Use the fully qualified System.Drawing.Rectangle to avoid ambiguity.
            System.Drawing.Rectangle popupRect = new System.Drawing.Rectangle(100, 100, 200, 200);

            // Create the popup annotation.
            // Parameters: rectangle, text content, initially open flag (false = hidden until hover), page number (1‑based).
            editor.CreatePopup(popupRect, "This is a note that appears when you hover over the area.", false, 1);

            // Save the modified document.
            editor.Save(outputPath);
            // Close the editor (optional, releases internal resources)
            editor.Close();
        }

        Console.WriteLine($"Popup annotation added. Saved to '{outputPath}'.");
    }
}