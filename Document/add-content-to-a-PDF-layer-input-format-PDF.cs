using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using Aspose.Pdf.Operators;

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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a new layer and add it to the page's layer collection
            Layer newLayer = new Layer("MyLayer", "OCG1");
            page.Layers.Add(newLayer);

            // Begin marked content for the new layer
            page.Contents.Add(new BMC(newLayer.Name));

            // Create a text fragment to be placed on the layer
            TextFragment tf = new TextFragment("Hello Layer");
            tf.Position = new Position(100, 500);
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Append the text fragment to the page (it will appear after the BMC operator)
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // End the marked content sequence
            page.Contents.Add(new EMC());

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with added layer saved to '{outputPath}'.");
    }
}