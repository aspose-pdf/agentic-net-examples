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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Appended text");
            // Set the position where the text will appear on the page
            tf.Position = new Position(100, 600);

            // Optional: customize appearance of the text
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Use TextBuilder to append the fragment to the page's paragraph collection
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF (saving as PDF does not require explicit SaveOptions)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved modified PDF to '{outputPath}'.");
    }
}