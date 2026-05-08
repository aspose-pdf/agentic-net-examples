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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Underlined text example");

            // Set the position where the text will appear on the first page
            fragment.Position = new Position(100, 700); // X=100, Y=700

            // Enable underlining via the TextState property
            fragment.TextState.Underline = true;

            // Optionally set other visual properties (font, size, color)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the fragment to the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(fragment);

            // Save the modified document (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underlined text added and saved to '{outputPath}'.");
    }
}