using System;
using System.IO;
using Aspose.Pdf;                         // Core PDF classes
using Aspose.Pdf.Facades;                 // Facade classes for editing

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the custom appearance PDF, and the result PDF
        const string sourcePdfPath      = "input.pdf";
        const string appearancePdfPath  = "appearance.pdf";
        const string outputPdfPath      = "output.pdf";

        // Ensure source files exist
        if (!File.Exists(sourcePdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }
        if (!File.Exists(appearancePdfPath))
        {
            Console.Error.WriteLine($"Appearance PDF not found: {appearancePdfPath}");
            return;
        }

        // Load the document to be annotated
        using (Document doc = new Document(sourcePdfPath))
        {
            // Initialize the content editor facade and bind the loaded document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Open the custom appearance PDF as a stream
            using (FileStream appearanceStream = File.OpenRead(appearancePdfPath))
            {
                // Define the rectangle where the annotation will appear (coordinates are in points)
                // PdfContentEditor.CreateRubberStamp expects a System.Drawing.Rectangle
                System.Drawing.Rectangle annotRect = new System.Drawing.Rectangle(100, 500, 200, 100);

                // Create a rubber‑stamp annotation on page 1 with a custom appearance stream
                // Parameters: page number (1‑based), rectangle, contents text, color, appearance stream
                editor.CreateRubberStamp(
                    page: 1,
                    annotRect: annotRect,
                    annotContents: "Custom Graphic Stamp",
                    color: System.Drawing.Color.Blue,
                    appearanceStream: appearanceStream);
            }

            // Save the modified document
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPdfPath}'.");
    }
}
