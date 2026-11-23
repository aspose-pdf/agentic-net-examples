using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "StruckConfidential.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the strike‑out annotation will appear
            // Coordinates are in points (1/72 inch). Adjust as needed.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create the strike‑out annotation
            StrikeOutAnnotation strikeOut = new StrikeOutAnnotation(page, rect)
            {
                // Use Subject to store author information (Author property does not exist)
                Subject  = "John Doe",
                Opacity  = 0.5,        // 50% opacity
                Title    = "Confidential",
                Contents = "This text is struck out."
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(strikeOut);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strike‑out annotation added and saved to '{outputPath}'.");
    }
}
