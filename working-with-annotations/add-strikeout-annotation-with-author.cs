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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the Document is disposed properly
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury) in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a strike‑out (strikethrough) annotation on the page
            StrikeOutAnnotation strike = new StrikeOutAnnotation(page, rect);

            // Set the author of the annotation
            strike.Title = "Jane Smith";

            // Optional: add a comment to the annotation
            strike.Contents = "Reviewed";

            // Attach the annotation to the page
            page.Annotations.Add(strike);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strikethrough annotation added. Saved to '{outputPath}'.");
    }
}