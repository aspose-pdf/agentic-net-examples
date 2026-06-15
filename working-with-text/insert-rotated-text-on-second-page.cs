using System;
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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The PDF does not contain a second page.");
                return;
            }

            // Get the second page (1‑based indexing)
            Page page = doc.Pages[2];

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Rotated Text");

            // Set the position where the text will start
            tf.Position = new Position(100, 500); // X = 100, Y = 500

            // Set text appearance (optional)
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Rotate the text fragment by 45 degrees
            tf.TextState.Rotation = 45;

            // Append the text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added to page 2 and saved as '{outputPath}'.");
    }
}