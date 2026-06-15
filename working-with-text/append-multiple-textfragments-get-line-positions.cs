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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Prepare a list of TextFragment objects, each representing a separate line
            var fragments = new System.Collections.Generic.List<TextFragment>();

            // ----- Line 1 -----
            TextFragment line1 = new TextFragment("First line of text");
            line1.Position = new Position(100, 700); // X, Y (baseline)
            line1.TextState.FontSize = 12;
            line1.TextState.Font = FontRepository.FindFont("Helvetica");
            line1.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            fragments.Add(line1);

            // ----- Line 2 -----
            TextFragment line2 = new TextFragment("Second line, longer text");
            line2.Position = new Position(100, 680);
            line2.TextState.FontSize = 12;
            line2.TextState.Font = FontRepository.FindFont("Helvetica");
            line2.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            fragments.Add(line2);

            // ----- Line 3 -----
            TextFragment line3 = new TextFragment("Third line");
            line3.Position = new Position(100, 660);
            line3.TextState.FontSize = 12;
            line3.TextState.Font = FontRepository.FindFont("Helvetica");
            line3.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            fragments.Add(line3);

            // Append all fragments to the page in one call
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragments);

            // Retrieve line‑break (Y‑coordinate) information for custom rendering
            Console.WriteLine("Line break positions:");
            foreach (TextFragment tf in fragments)
            {
                // The YIndent of the Position struct represents the baseline Y coordinate
                float baselineY = (float)tf.Position.YIndent; // explicit cast from double to float
                Console.WriteLine($"\"{tf.Text}\" at Y = {baselineY}");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}
