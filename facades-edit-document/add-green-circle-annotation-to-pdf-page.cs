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
        const string outputPath = "output_with_circle.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfAnnotationEditor (a Facades class) to bind, modify, and save the PDF.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the PDF document.
            editor.BindPdf(inputPath);

            // Access the underlying Document object.
            Document doc = editor.Document;

            // Ensure the document has at least six pages.
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document does not contain page 6.");
                return;
            }

            // Page six (Aspose.Pdf uses 1‑based indexing).
            Page page = doc.Pages[6];

            // Define the rectangle that will surround the diagram.
            // Adjust the coordinates (llx, lly, urx, ury) as needed.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 800);

            // Create a circle annotation on the specified page and rectangle.
            CircleAnnotation circle = new CircleAnnotation(page, rect)
            {
                // Set the outline (stroke) color to green.
                Color = Aspose.Pdf.Color.Green
            };

            // Set a thick border (e.g., 5 points).
            circle.Border = new Border(circle) { Width = 5 };

            // Add the annotation to the page.
            page.Annotations.Add(circle);

            // Save the modified PDF using the Facades editor.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}