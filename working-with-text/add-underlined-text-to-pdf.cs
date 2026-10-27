using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "underlined_output.pdf";
        const string text = "Underlined text example";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add underlined text, and save.
        using (Document doc = new Document(inputPath))
        {
            // Create a text fragment with the desired content.
            TextFragment fragment = new TextFragment(text);
            // Enable underline via the TextState.
            fragment.TextState.Underline = true;
            // Optional styling.
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Insert the fragment into the first page (1‑based indexing).
            Page page = doc.Pages[1];
            page.Paragraphs.Add(fragment);

            // Save the modified document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Underlined text added and saved to '{outputPath}'.");
    }
}