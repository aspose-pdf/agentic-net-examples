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
        const string author = "John Doe";
        const string note = "Review this section.";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates in points)
            var rect = new Rectangle(100, 500, 300, 550);

            // Create a text annotation with custom author metadata
            var txtAnnot = new TextAnnotation(page, rect)
            {
                Title    = author,   // custom author metadata
                Contents = note,
                Open     = true,
                Icon     = TextIcon.Note
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(txtAnnot);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotation added and saved to '{outputPath}'.");
    }
}
