using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "SquigglyStyled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least 15 pages
            if (doc.Pages.Count < 15)
            {
                Console.Error.WriteLine("The document has fewer than 15 pages.");
                return;
            }

            // Get page 15 (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[15];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a SquigglyAnnotation with the rectangle
            SquigglyAnnotation squiggly = new SquigglyAnnotation(page, rect)
            {
                // Set pink color using Aspose.Pdf.Color (cross‑platform)
                Color   = Aspose.Pdf.Color.FromRgb(1.0, 0.75, 0.8), // pink
                Opacity = 0.6
            };

            // Add the annotation to the page
            page.Annotations.Add(squiggly);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Squiggly annotation added and saved to '{outputPath}'.");
    }
}