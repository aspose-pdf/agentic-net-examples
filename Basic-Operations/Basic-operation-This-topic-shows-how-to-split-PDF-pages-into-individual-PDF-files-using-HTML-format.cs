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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Define the rectangle that bounds the annotation (coordinates in points)
            // Example rectangle: lower‑left (100,100), upper‑right (200,150)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);

            // Create a text annotation on the first page
            TextAnnotation textAnnot = new TextAnnotation(pdfDocument.Pages[1], rect);
            textAnnot.Contents = "Sample Title";          // tooltip / title
            textAnnot.Icon = TextIcon.Comment;            // use the updated enum

            // Initialize the border (border‑initialization rule)
            textAnnot.Border = new Border(textAnnot)
            {
                Style = BorderStyle.Solid,
                Width = 1
            };

            // Set the annotation color (using Aspose.Pdf.Color to avoid ambiguity)
            textAnnot.Color = Aspose.Pdf.Color.Blue;

            // Add the annotation to the page
            pdfDocument.Pages[1].Annotations.Add(textAnnot);

            // Save the modified PDF (document‑save rule)
            pdfDocument.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}