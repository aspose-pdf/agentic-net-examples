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

        // Load the PDF, add a highlight annotation, export to XFDF, and save the PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the highlight will appear
            // Fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the highlight annotation
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Set the highlight color (yellow is typical)
                Color = Aspose.Pdf.Color.Yellow,

                // Optional: add a comment that appears in the popup
                Contents = "Important text highlighted."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Export all annotations (including the newly added highlight) to XFDF (XML)
            doc.ExportAnnotationsToXfdf(xfdfPath);

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Highlight added and PDF saved to '{outputPdfPath}'.");
        Console.WriteLine($"Annotations exported to XFDF file '{xfdfPath}'.");

        // Demonstrate importing the XFDF back into a new document (optional)
        const string importedPdfPath = "imported_highlight.pdf";

        using (Document doc = new Document(inputPdfPath))
        {
            // Import annotations from the previously saved XFDF file
            doc.ImportAnnotationsFromXfdf(xfdfPath);

            // Save the document with imported annotations
            doc.Save(importedPdfPath);
        }

        Console.WriteLine($"Annotations imported and PDF saved to '{importedPdfPath}'.");
    }
}