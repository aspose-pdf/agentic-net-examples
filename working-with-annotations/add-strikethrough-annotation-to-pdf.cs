using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_strikethrough.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page that contains the paragraph (example: first page)
            Page page = doc.Pages[1];

            // Define the rectangle that covers the paragraph.
            // Coordinates are in points (1/72 inch) with origin at lower‑left.
            // Adjust llx, lly, urx, ury to match the actual paragraph location.
            double llx = 100; // lower‑left X
            double lly = 500; // lower‑left Y
            double urx = 400; // upper‑right X
            double ury = 520; // upper‑right Y
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create a strike‑out annotation over the rectangle
            StrikeOutAnnotation strikeOut = new StrikeOutAnnotation(page, rect)
            {
                // Set the author of the annotation (PDF uses the Title field for author)
                Title = "reviewer",
                // Optional: set the color of the strike‑out line
                Color = Aspose.Pdf.Color.Red,
                // Optional: add a comment that appears in the annotation popup
                Contents = "Paragraph marked for review"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(strikeOut);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strikethrough annotation added and saved to '{outputPath}'.");
    }
}