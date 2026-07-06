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

        using (Document doc = new Document(inputPath))
        {
            // Verify that the document has at least two pages (1‑based indexing)
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Get the second page
            Page page = doc.Pages[2];

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Rotated Text");
            tf.Position = new Position(100, 500); // baseline position

            // Set font and color
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 24;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Apply rotation (in degrees)
            tf.TextState.Rotation = 45;

            // Append the rotated text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added to page 2 and saved as '{outputPath}'.");
    }
}