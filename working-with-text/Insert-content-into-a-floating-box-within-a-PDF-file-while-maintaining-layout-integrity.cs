using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a floating box with explicit width and height
            FloatingBox box = new FloatingBox(200, 100);
            // Position the box on the page (absolute coordinates)
            box.Left = 100; // distance from the left edge
            box.Top = 500;  // distance from the top edge

            // Optional visual styling
            box.BackgroundColor = Aspose.Pdf.Color.LightGray;
            // Corrected: use BorderSide enum directly (not nested in BorderInfo)
            box.Border = new BorderInfo(BorderSide.All, 1, Aspose.Pdf.Color.Black);

            // Create a text fragment that will be placed inside the floating box
            TextFragment tf = new TextFragment("Hello from FloatingBox!");
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the text fragment to the floating box's paragraph collection
            box.Paragraphs.Add(tf);

            // Add the floating box to the page's paragraphs collection
            page.Paragraphs.Add(box);

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Floating box added and saved to '{outputPath}'.");
    }
}
