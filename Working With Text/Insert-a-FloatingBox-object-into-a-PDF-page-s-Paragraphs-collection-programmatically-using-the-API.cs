using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "floatingbox_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a FloatingBox with specific width and height
            FloatingBox box = new FloatingBox(200f, 100f)
            {
                // Position the box absolutely at (100, 500) on the page
                PositioningMode = ParagraphPositioningMode.Absolute,
                Left = 100f,
                Top  = 500f,

                // Optional visual styling
                BackgroundColor = Color.LightGray,
                Border = new BorderInfo(BorderSide.All, 1f, Color.Black)
            };

            // Add a simple text fragment inside the floating box
            TextFragment txt = new TextFragment("This is a FloatingBox with text.");
            box.Paragraphs.Add(txt);

            // Insert the FloatingBox into the page's Paragraphs collection
            page.Paragraphs.Add(box);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"FloatingBox added and saved to '{outputPath}'.");
    }
}
