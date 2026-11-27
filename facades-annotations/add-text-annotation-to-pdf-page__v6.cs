using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "annotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define the annotation rectangle (coordinates in points)
        // lower‑left (100, 500), upper‑right (300, 550)
        Rectangle rect = new Rectangle(100, 500, 300, 550);

        // Get the first page (pages are 1‑based)
        Page page = doc.Pages[1];

        // Create a TextAnnotation and set its properties
        TextAnnotation txtAnno = new TextAnnotation(page, rect)
        {
            Subject = "Review",
            Contents = "Please review this section.",
            Title = "Reviewer"
        };

        // Add the annotation to the page
        page.Annotations.Add(txtAnno);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
