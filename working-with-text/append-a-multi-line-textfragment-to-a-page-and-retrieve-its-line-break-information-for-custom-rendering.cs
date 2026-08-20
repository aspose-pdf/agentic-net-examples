using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define multi‑line text
            string[] lines = new[]
            {
                "First line of text",
                "Second line of text",
                "Third line of text"
            };

            // Prepare a list of TextFragment objects, one per line
            List<TextFragment> fragments = new List<TextFragment>();
            // Starting position (bottom‑left corner of the first line)
            float startX = 100f;
            float startY = 600f;
            // Line spacing (distance between baselines)
            float lineSpacing = 12f;

            for (int i = 0; i < lines.Length; i++)
            {
                TextFragment tf = new TextFragment(lines[i]);
                // Position each line using the baseline Y coordinate
                tf.Position = new Position(startX, startY - i * lineSpacing);
                // Example styling (optional)
                tf.TextState.FontSize = 10;
                tf.TextState.Font = FontRepository.FindFont("TimesNewRoman");
                tf.TextState.ForegroundColor = Color.Black;

                fragments.Add(tf);
            }

            // Append all fragments to the page in a single operation
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragments);

            // Retrieve line‑break information (baseline Y positions) for custom rendering
            Console.WriteLine("Line‑break positions (baseline Y):");
            for (int i = 0; i < fragments.Count; i++)
            {
                TextFragment tf = fragments[i];
                // The YIndent of the Position struct represents the baseline coordinate
                Console.WriteLine($"Line {i + 1}: Y = {tf.Position.YIndent}");
            }

            // Save the modified document (Document.Save without SaveOptions writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}