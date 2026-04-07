using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for TextFragment, Position, FontRepository, etc.

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Get the second page (1‑based indexing)
            Page page = doc.Pages[2];

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Rotated Text");
            // Position the text on the page
            tf.Position = new Position(100, 500);
            // Set font, size and colors
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 24;
            tf.TextState.ForegroundColor = Color.Blue;
            tf.TextState.BackgroundColor = Color.LightGray;
            // Rotate the text (angle in degrees)
            tf.TextState.Rotation = 45;

            // Use TextBuilder to append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added and saved to '{outputPath}'.");
    }
}