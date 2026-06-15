using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (facade) to edit the PDF.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // Define the stamp rectangle (position and size) using System.Drawing.Rectangle.
            System.Drawing.Rectangle stampRect = new System.Drawing.Rectangle(100, 500, 200, 100);

            // Create a rubber‑stamp annotation.
            // Parameters: page number (1‑based), rectangle, icon name, stamp text, border color.
            editor.CreateRubberStamp(
                page: 1,
                annotRect: stampRect,
                icon: "Draft",
                annotContents: "Important Section",
                color: System.Drawing.Color.Red);

            // After creation, retrieve the annotation to customize its border.
            Document doc = editor.Document;                     // The underlying Document.
            Page page = doc.Pages[1];                           // First page (1‑based index).

            if (page.Annotations.Count > 0)
            {
                // The newly added annotation is the last one in the collection.
                Annotation lastAnnot = page.Annotations[page.Annotations.Count];
                if (lastAnnot is StampAnnotation stampAnnot)
                {
                    // Set a custom border thickness (width = 2 points).
                    stampAnnot.Border = new Border(stampAnnot) { Width = 2 };

                    // Optionally change the stamp's interior color.
                    stampAnnot.Color = Aspose.Pdf.Color.Blue;
                }
            }

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}