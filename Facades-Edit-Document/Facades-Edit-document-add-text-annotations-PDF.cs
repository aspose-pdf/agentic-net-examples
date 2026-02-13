using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Paths for input and output PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Choose the page where the annotation will be placed (1‑based index)
            Page page = pdfDocument.Pages[1];

            // Define the rectangle that bounds the annotation (llx, lly, urx, ury)
            // Fully qualify the Rectangle type to avoid ambiguity between Aspose.Pdf.Rectangle and Aspose.Pdf.Drawing.Rectangle
            Aspose.Pdf.Rectangle annotationRect = new Aspose.Pdf.Rectangle(100, 100, 200, 200);

            // Create a new TextAnnotation (sticky note) on the selected page
            TextAnnotation textAnnot = new TextAnnotation(page, annotationRect);

            // Set annotation properties
            textAnnot.Title = "Sample Note";
            textAnnot.Contents = "This is a text annotation added via Aspose.Pdf.Facades.";
            textAnnot.Color = Color.Yellow; // Background color of the sticky note

            // Initialize the border after the annotation object has been created
            textAnnot.Border = new Border(textAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(textAnnot);

            // Save the modified PDF document
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Annotation added successfully. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
