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

        // Load the existing PDF document inside a using block (ensures disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a new layer with a name and an optional content group ID
            Layer myLayer = new Layer("MyTextLayer", "OCG1");

            // Add the layer to the page's layer collection
            // (Page.Layers is a collection of Layer objects)
            page.Layers.Add(myLayer);

            // Create a text fragment that will be placed on the page
            TextFragment tf = new TextFragment("Hello, Layered Text!");
            tf.Position = new Position(100, 600); // X=100, Y=600

            // Optionally set text appearance
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Color.Blue;

            // Append the text fragment to the page using TextBuilder
            // The text will be part of the page content; because the layer is active,
            // the text is associated with the newly created layer.
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with added layer saved to '{outputPath}'.");
    }
}