using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a strike‑out (strikethrough) annotation on the page
            StrikeOutAnnotation strike = new StrikeOutAnnotation(page, rect)
            {
                // Set the author of the annotation
                Title = "Jane Smith"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(strike);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strikethrough annotation author set and saved to '{outputPath}'.");
    }
}