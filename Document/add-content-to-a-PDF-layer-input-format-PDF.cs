using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_layer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a new layer with a name and an optional content group ID
            Layer newLayer = new Layer("MyLayer", "OCG1");

            // Add the layer to the page's layer collection
            page.Layers.Add(newLayer);

            // Add some visible content to the page.
            // This example adds a simple text fragment.
            TextFragment tf = new TextFragment("Hello from new layer!");
            tf.Position = new Position(100, 600);
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Use TextBuilder to append the text fragment to the page.
            // The content will be part of the page's content stream,
            // which now belongs to the newly created layer.
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with added layer saved to '{outputPath}'.");
    }
}