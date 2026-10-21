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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the annotation rectangle (lower‑left x,y and upper‑right x,y)
            // Here we place the annotation at (100,200) with a size of 100x50 points
            Rectangle rect = new Rectangle(100, 200, 200, 250);

            // Create a text annotation bound to the first page
            TextAnnotation textAnn = new TextAnnotation(doc.Pages[1], rect)
            {
                Title = "Note",
                Contents = "Custom annotation content"
            };

            // Add the annotation to page 1 (pages are 1‑based)
            doc.Pages[1].Annotations.Add(textAnn);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
