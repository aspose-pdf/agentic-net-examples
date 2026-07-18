using System;
using System.IO;
using System.Drawing;               // System.Drawing.Rectangle for the link area
using Aspose.Pdf;                  // Core PDF classes
using Aspose.Pdf.Facades;          // PdfContentEditor facade

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document – wrapped in using for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the content editor and bind the loaded document
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                editor.BindPdf(doc);

                // Define the clickable rectangle (x, y, width, height) in points
                // Use System.Drawing.Rectangle because PdfContentEditor.CreateLocalLink expects it
                System.Drawing.Rectangle linkRect = new System.Drawing.Rectangle(100, 700, 200, 50); // adjust as needed

                int originalPage   = 1; // page where the link will appear (1‑based)
                int destinationPage = 3; // page to navigate to (1‑based)

                // Create a local link that jumps to the destination page
                editor.CreateLocalLink(linkRect, destinationPage, originalPage);

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Hyperlink annotation added. Saved to '{outputPath}'.");
    }
}
