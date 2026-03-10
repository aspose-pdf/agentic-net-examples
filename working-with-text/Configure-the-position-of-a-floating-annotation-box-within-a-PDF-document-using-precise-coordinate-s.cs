using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Annotations;   // For annotation types (if needed)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Select the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a FloatingBox with explicit width and height
            FloatingBox floatingBox = new FloatingBox(200f, 100f); // width = 200, height = 100

            // Use absolute positioning to place the box at exact coordinates
            floatingBox.PositioningMode = ParagraphPositioningMode.Absolute;

            // Set the exact location (coordinates are measured from the lower‑left corner of the page)
            floatingBox.Left = 100f; // X coordinate
            floatingBox.Top  = 500f; // Y coordinate

            // Optional visual styling
            floatingBox.BackgroundColor = Aspose.Pdf.Color.LightGray;

            // Add the FloatingBox to the page's paragraph collection
            page.Paragraphs.Add(floatingBox);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Floating box positioned and saved to '{outputPath}'.");
    }
}