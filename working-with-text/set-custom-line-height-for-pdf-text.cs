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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add a new page to the document
            Page page = doc.Pages.Add();

            // Create a text fragment with multiple lines
            TextFragment fragment = new TextFragment("First line\nSecond line\nThird line");

            // Set a custom line height (line spacing) for the text
            // NOTE: The correct property is LineSpacing, not LineHeight.
            fragment.TextState.LineSpacing = 20f; // 20 points between lines

            // Optional: set font, size and a color for clarity
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the text fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom line height to '{outputPath}'.");
    }
}
