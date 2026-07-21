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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment with the content to insert
            TextFragment tf = new TextFragment("Sample text with custom word spacing");

            // Configure the TextState: font, size, color, and word spacing
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;
            tf.TextState.WordSpacing = 2.0f; // increase spacing between words

            // Set the position where the text will appear on the page
            tf.Position = new Position(100, 700);

            // Add the configured text fragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Save the modified PDF to the specified output path
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}