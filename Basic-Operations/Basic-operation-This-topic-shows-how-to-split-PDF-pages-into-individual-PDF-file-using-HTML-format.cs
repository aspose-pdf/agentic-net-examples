using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPath}'.");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Ensure the document has at least one page
            if (pdfDocument.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The PDF does not contain any pages.");
                return;
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = pdfDocument.Pages[1];

            // Define the rectangle that bounds the text annotation
            // (llx, lly, urx, ury) – adjust as needed
            var annotationRect = new Aspose.Pdf.Rectangle(100, 500, 200, 600);

            // Create the text annotation
            TextAnnotation textAnnot = new TextAnnotation(page, annotationRect);

            // Set the annotation's visible text (title/tooltip)
            textAnnot.Contents = "Sample Title";

            // Set the icon – use the current enum TextIcon
            textAnnot.Icon = TextIcon.Comment;

            // Set the annotation color (optional)
            textAnnot.Color = Aspose.Pdf.Color.Blue;

            // Initialize the border after the annotation object (border‑initialization rule)
            textAnnot.Border = new Border(textAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation to the page
            page.Annotations.Add(textAnnot);

            // Save the modified PDF (document‑save rule)
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Annotation added successfully. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}