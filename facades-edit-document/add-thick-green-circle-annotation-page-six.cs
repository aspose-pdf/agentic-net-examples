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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use the PdfAnnotationEditor facade to bind, modify, and save the PDF.
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Access the underlying Document.
            Document doc = editor.Document;

            // Ensure the document has at least six pages.
            if (doc.Pages.Count < 6)
            {
                Console.Error.WriteLine("The document does not contain page 6.");
                return;
            }

            // Page six (1‑based indexing).
            Page page = doc.Pages[6];

            // Define the rectangle that will surround the diagram.
            // Constructor: new Aspose.Pdf.Rectangle(llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 200, 400, 500);

            // Create a circle annotation on the specified page and rectangle.
            CircleAnnotation circle = new CircleAnnotation(page, rect)
            {
                // Outline color (green) and optional contents.
                Color    = Aspose.Pdf.Color.Green,
                Contents = "Diagram"
            };

            // Set a thick border (e.g., 5 points).
            circle.Border = new Border(circle) { Width = 5 };

            // Add the annotation to the page.
            page.Annotations.Add(circle);

            // Save the modified PDF.
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}