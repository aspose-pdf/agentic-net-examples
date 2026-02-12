using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdf);

            // Ensure the document has at least one page
            if (pdfDocument.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The PDF does not contain any pages.");
                return;
            }

            // Choose the first page (1‑based indexing)
            Page page = pdfDocument.Pages[1];

            // Define the rectangle where the annotation will appear
            // (llx, lly, urx, ury) – coordinates are in points
            var rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation
            TextAnnotation textAnnot = new TextAnnotation(page, rect)
            {
                // Title shown in the annotation's popup window
                Title = "Sample Title",
                // The visible text of the annotation
                Contents = "This is a text annotation added by Aspose.Pdf.",
                // Icon displayed on the page (Comment, Note, etc.)
                Icon = TextIcon.Comment,
                // Color of the annotation border
                Color = Color.Blue
            };

            // Initialize the border using the provided rule
            // Border must be set after the annotation object is created
            textAnnot.Border = new Border(textAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation to the page
            page.Annotations.Add(textAnnot);

            // Save the modified PDF
            pdfDocument.Save(outputPdf);

            Console.WriteLine($"Annotation added successfully. Output saved to: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
