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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Get the second page (page indexing is 1‑based)
            Page page = doc.Pages[2];

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Rotated Text");
            tf.Position = new Position(100, 500); // position on the page

            // Set text appearance
            tf.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Rotate the text fragment (degrees)
            tf.TextState.Rotation = 45;

            // Use TextBuilder to append the rotated text fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF (lifecycle rule: use using, PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Rotated text added to page 2 and saved as '{outputPath}'.");
    }
}