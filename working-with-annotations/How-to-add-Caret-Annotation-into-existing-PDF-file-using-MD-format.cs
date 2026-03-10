using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_caret.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the caret annotation will be placed (1‑based index)
            Page page = doc.Pages[1];

            // Define the rectangle that bounds the caret annotation.
            // Fully qualified type is used to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 120, 520);

            // Create the caret annotation on the selected page.
            CaretAnnotation caret = new CaretAnnotation(page, rect)
            {
                // Example markdown content (the annotation's popup will show this text)
                Contents = "This is **bold** and *italic* markdown text.",
                // Set a visible color for the caret border
                Color = Aspose.Pdf.Color.Red,
                // Choose a symbol; Paragraph adds a paragraph symbol next to the caret
                Symbol = CaretSymbol.Paragraph
            };

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add(caret);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Caret annotation added. Saved to '{outputPath}'.");
    }
}