using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "centered.pdf";
        const string content = "Centered Text";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Use the first page (or add a new one if needed)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment(content);

            // Center the text horizontally on the page
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Position the fragment vertically (X is ignored when centered)
            fragment.Position = new Position(0, 500);

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Centered text PDF saved to '{outputPath}'.");
    }
}