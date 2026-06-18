using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // Included as per requirement

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document within a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a markup annotation (TextAnnotation) on the page
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Created with UTC timestamp",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true
            };

            // Set the creation date to the current UTC time
            annotation.CreationDate = DateTime.UtcNow;

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(annotation);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation with UTC creation date saved to '{outputPath}'.");
    }
}