using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_strikeout.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Assume the paragraph is on the first page; define its rectangle (llx, lly, urx, ury)
            // Adjust the coordinates as needed to cover the target paragraph.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 520);

            // Create a StrikeOut annotation on the specified page and rectangle
            StrikeOutAnnotation strike = new StrikeOutAnnotation(doc.Pages[1], rect)
            {
                // Set the author (reviewer) of the annotation
                Title = "reviewer",
                // Optional: set the color of the strikeout line
                Color = Aspose.Pdf.Color.Red
            };

            // Add the annotation to the page's annotation collection
            doc.Pages[1].Annotations.Add(strike);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Strikethrough annotation added. Saved to '{outputPath}'.");
    }
}