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
        const string outputPath = "output_md_annotation.pdf";

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (left, bottom, right, top)
            // Fully qualified type name avoids ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 520);

            // Create a Highlight annotation (concrete subclass of TextMarkupAnnotation)
            HighlightAnnotation highlight = new HighlightAnnotation(page, rect)
            {
                // Tooltip text shown when the mouse hovers over the annotation
                Contents = "Highlighted text (MD format)",
                // Set the visual color of the highlight
                Color = Aspose.Pdf.Color.Yellow,
                // Store markdown content in the RichText property (Aspose.Pdf treats this as a string)
                RichText = "**Bold** _italic_ `code`"
            };

            // Add the annotation to the page's annotation collection
            page.Annotations.Add(highlight);

            // Save the modified PDF (lifecycle rule: use doc.Save within the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with markdown text markup annotation saved to '{outputPath}'.");
    }
}