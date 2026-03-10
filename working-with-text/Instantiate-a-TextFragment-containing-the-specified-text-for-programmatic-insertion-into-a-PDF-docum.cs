using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the output PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // The text that will be placed into the PDF
        const string fragmentText = "Your specified text here";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextFragment initialized with the desired text
            TextFragment tf = new TextFragment(fragmentText);

            // Optionally set the position where the text will appear
            // Position uses Aspose.Pdf.Text.Position (XIndent, YIndent)
            tf.Position = new Position(100, 600);

            // Append the TextFragment to the first page using TextBuilder
            Page page = doc.Pages[1]; // Pages are 1‑based
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"TextFragment added and saved to '{outputPath}'.");
    }
}