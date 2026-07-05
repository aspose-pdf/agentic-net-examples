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

        // Load the existing PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a new text fragment with the desired content
            TextFragment fragment = new TextFragment("Underlined text example");

            // Position the fragment on the page (coordinates are in points)
            fragment.Position = new Position(100, 700);

            // Set the underline property before adding the fragment to the page
            fragment.TextState.Underline = true;

            // Optionally set other text properties (font, size, color)
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.ForegroundColor = Color.Blue;

            // Append the fragment to the first page using TextBuilder
            Page page = doc.Pages[1];
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified document (using the standard Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underlined text added and saved to '{outputPath}'.");
    }
}