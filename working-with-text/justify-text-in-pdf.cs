using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "justified_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Use the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // Create a text fragment with the content to justify
            TextFragment fragment = new TextFragment(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
            );

            // Set horizontal alignment to Justify during creation
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Justify;

            // Optional styling
            fragment.TextState.FontSize = 12;
            fragment.Margin = new MarginInfo { Top = 20, Bottom = 20, Left = 40, Right = 40 };

            // Add the fragment to the page
            page.Paragraphs.Add(fragment);

            // Save the modified PDF (PDF format is default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Justified PDF saved to '{outputPath}'.");
    }
}