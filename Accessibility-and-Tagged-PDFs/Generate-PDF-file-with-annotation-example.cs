using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
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

            // Choose the page to add the annotation (first page in this example)
            Page page = pdfDocument.Pages[1];

            // Define the rectangle where the annotation will appear
            // (llx, lly, urx, ury) in points
            var rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Create a TextAnnotation on the selected page
            TextAnnotation textAnnot = new TextAnnotation(page, rect);

            // Set the title (displayed in the annotation's popup window title bar)
            textAnnot.Title = "Sample Title";

            // Optional: set the contents (text shown inside the popup)
            textAnnot.Contents = "This is a text annotation added via Aspose.Pdf.";

            // Optional: set the annotation color
            textAnnot.Color = Color.Yellow;

            // Initialize the border using the provided rule
            // Note: Border must be set after the annotation object is created
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