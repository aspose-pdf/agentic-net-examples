using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

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
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // ---------- Add a text fragment ----------
            TextFragment tf = new TextFragment("Added accessible text");
            tf.Position = new Position(100, 700); // coordinates in points
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            page.Paragraphs.Add(tf);

            // ---------- Add a highlight annotation ----------
            // Define the rectangle that the highlight will cover
            Aspose.Pdf.Rectangle highlightRect = new Aspose.Pdf.Rectangle(100, 650, 300, 670);
            HighlightAnnotation highlight = new HighlightAnnotation(page, highlightRect);
            highlight.Color = Aspose.Pdf.Color.Yellow; // highlight color
            page.Annotations.Add(highlight);

            // Store the 1‑based index of the newly added highlight (last annotation)
            int highlightIndex = page.Annotations.Count; // AnnotationCollection is 1‑based

            // ---------- Add a figure‑like annotation (using SquareAnnotation) ----------
            // Rectangle for the figure (image placeholder)
            Aspose.Pdf.Rectangle figureRect = new Aspose.Pdf.Rectangle(100, 500, 300, 620);
            SquareAnnotation figure = new SquareAnnotation(page, figureRect);
            // Use Contents to store the description (alternative text) for the figure annotation
            figure.Contents = "Description of the figure annotation";
            page.Annotations.Add(figure);

            // ---------- Delete the highlight annotation by its index ----------
            // Ensure the index is within bounds and the annotation is a HighlightAnnotation
            if (highlightIndex >= 1 && highlightIndex <= page.Annotations.Count &&
                page.Annotations[highlightIndex] is HighlightAnnotation)
            {
                page.Annotations.Delete(highlightIndex);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF processed and saved to '{outputPath}'.");
    }
}
