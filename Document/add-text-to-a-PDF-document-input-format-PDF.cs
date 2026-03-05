using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextFragment, TextBuilder, Position, FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");
            tf.Position = new Position(100, 600); // place the text at (100,600)

            // Optional: set font, size and colors
            tf.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            tf.TextState.FontSize = 12;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Append the text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified document (lifecycle rule: use Save without extra options for PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text added and saved to '{outputPath}'.");
    }
}