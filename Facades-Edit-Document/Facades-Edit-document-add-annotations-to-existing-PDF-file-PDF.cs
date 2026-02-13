using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDoc = new Document(inputPath);

            // Define the rectangle where the annotation will appear (coordinates are in points)
            var rect = new Rectangle(100, 500, 300, 600);

            // Create a free‑text annotation on the first page
            var textAnnot = new TextAnnotation(pdfDoc.Pages[1], rect)
            {
                Contents = "Annotation added via Aspose.Pdf.",
                Color = Color.Yellow // Background color of the annotation
            };

            // Set the border for the annotation
            textAnnot.Border = new Border(textAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Add the annotation directly to the page's annotation collection
            pdfDoc.Pages[1].Annotations.Add(textAnnot);

            // Save the modified PDF
            pdfDoc.Save(outputPath);
            Console.WriteLine($"Annotation added and saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
