using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class HighlightAnnotationExample
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputPdfPath  = "highlighted.pdf";    // PDF with highlight
        const string xfdfPath       = "annotations.xfdf";   // XFDF (XML) file

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the PDF and add a HighlightAnnotation to page 1
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the highlight will appear
            // Fully qualified type name avoids ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 700, 400, 720);

            // Create the highlight annotation
            HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect)
            {
                // Set a semi‑transparent yellow color
                Color   = Aspose.Pdf.Color.Yellow,
                Opacity = 0.5,
                // Optional: add a comment that appears in the pop‑up
                Contents = "Important text highlighted."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the PDF with the new annotation
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Highlight added and saved to '{outputPdfPath}'.");

        // ------------------------------------------------------------
        // 2. Export all annotations from the PDF to an XFDF (XML) file
        // ------------------------------------------------------------
        using (Document docWithHighlight = new Document(outputPdfPath))
        {
            docWithHighlight.ExportAnnotationsToXfdf(xfdfPath);
        }

        Console.WriteLine($"Annotations exported to XFDF file '{xfdfPath}'.");

        // ------------------------------------------------------------
        // 3. Create a fresh PDF (could be empty or another source) and
        //    import the previously exported annotations back into it
        // ------------------------------------------------------------
        using (Document freshDoc = new Document(inputPdfPath)) // reuse the original PDF
        {
            freshDoc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the document that now contains the imported annotations
            const string importedPdfPath = "imported_highlights.pdf";
            freshDoc.Save(importedPdfPath);

            Console.WriteLine($"Annotations imported and saved to '{importedPdfPath}'.");
        }
    }
}