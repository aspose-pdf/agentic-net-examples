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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Verify that page 8 exists (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The document has fewer than 8 pages.");
                return;
            }

            Page page = doc.Pages[8];

            // ----- Add a Text Annotation -----
            // Rectangle coordinates: lower‑left x, lower‑left y, upper‑right x, upper‑right y
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation textAnn = new TextAnnotation(page, textRect)
            {
                Title = "Note",
                Contents = "This is a text annotation.",
                Open = true,
                Icon = TextIcon.Note
            };
            page.Annotations.Add(textAnn);

            // ----- Add a Highlight Annotation -----
            // Define the area to be highlighted
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 400, 300, 420);
            HighlightAnnotation highlightAnn = new HighlightAnnotation(page, highlightRect)
            {
                // Set the highlight color (using Aspose.Pdf.Color)
                Color = Aspose.Pdf.Color.Yellow
            };
            page.Annotations.Add(highlightAnn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotations added and saved to '{outputPath}'.");
    }
}