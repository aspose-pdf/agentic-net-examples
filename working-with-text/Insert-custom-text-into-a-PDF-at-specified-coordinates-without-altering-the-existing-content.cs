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
        const string customText = "Hello, Aspose!";
        // Desired coordinates (X, Y) on the page
        double x = 100;
        double y = 600;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document (wrapped in using for proper disposal)
            using (Document doc = new Document(inputPath))
            {
                // Access the first page (Aspose.Pdf uses 1‑based indexing)
                Page page = doc.Pages[1];

                // Create a text fragment with the custom string
                TextFragment tf = new TextFragment(customText);
                // Set the position where the text will be placed
                tf.Position = new Position(x, y);
                // Optional styling
                tf.TextState.FontSize = 12;
                tf.TextState.Font = FontRepository.FindFont("TimesNewRoman");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

                // Append the text fragment to the page using TextBuilder
                TextBuilder builder = new TextBuilder(page);
                builder.AppendText(tf);

                // Save the modified PDF (saving as PDF does not require explicit SaveOptions)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Custom text added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}