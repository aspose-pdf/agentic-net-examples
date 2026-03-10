using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for ParagraphPositioningMode enum

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_floatingbox.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a floating box with explicit width and height
            FloatingBox box = new FloatingBox(200f, 100f);

            // Position the box absolutely so it does not affect the flow of other content
            box.PositioningMode = ParagraphPositioningMode.Absolute;

            // Set the location on the page (coordinates are in points, origin at bottom‑left)
            box.Left = 100f;   // distance from the left edge of the page
            box.Top  = 500f;   // distance from the bottom edge of the page

            // Optional visual styling
            box.BackgroundColor = Aspose.Pdf.Color.LightGray;
            box.Border = new BorderInfo(BorderSide.All, 1f, Aspose.Pdf.Color.Black);

            // Add the floating box to the page's paragraph collection
            page.Paragraphs.Add(box);

            // Save the modified document (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Floating box added and saved to '{outputPath}'.");
    }
}