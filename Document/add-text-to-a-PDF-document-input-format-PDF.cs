using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // required for TextFragment, TextBuilder, Position

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

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
            {
                // Add a blank page if the document is empty
                doc.Pages.Add();
            }

            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf!");
            // Position the text on the page (coordinates are in points)
            fragment.Position = new Position(100, 600);

            // Optional: set basic text appearance
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("TimesNewRoman");
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Append the text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified document (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text added and saved to '{outputPath}'.");
    }
}