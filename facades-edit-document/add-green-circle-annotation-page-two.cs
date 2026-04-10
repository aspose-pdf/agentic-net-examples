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

        // Initialize the Facade and bind the PDF document
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(inputPath);

            // Access the underlying Document
            Document doc = editor.Document;

            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Define the annotation rectangle (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a circle annotation on page 2
            Page page = doc.Pages[2];
            CircleAnnotation circle = new CircleAnnotation(page, rect)
            {
                // Set the border (stroke) color
                Color = Aspose.Pdf.Color.Green,
                // Set the interior (fill) color
                InteriorColor = Aspose.Pdf.Color.Green
            };

            // Set the border width to 3 points
            circle.Border = new Border(circle) { Width = 3 };

            // Add the annotation to the page
            page.Annotations.Add(circle);

            // Save the modified PDF
            editor.Save(outputPath);
            editor.Close();
        }

        Console.WriteLine($"Circle annotation added and saved to '{outputPath}'.");
    }
}