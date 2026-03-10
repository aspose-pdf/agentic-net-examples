using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertCustomText
{
    static void Main()
    {
        // Input PDF path, output PDF path, and the page number where text will be inserted
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int targetPageNumber = 2; // 1‑based index

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the requested page exists
            if (targetPageNumber < 1 || targetPageNumber > doc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {targetPageNumber} is out of range. Document has {doc.Pages.Count} pages.");
                return;
            }

            // Get the target page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[targetPageNumber];

            // Create a text fragment with the custom content
            TextFragment fragment = new TextFragment("Custom inserted text");

            // Set the position where the text will appear (coordinates are in points, origin at bottom‑left)
            fragment.Position = new Position(100, 500); // adjust X/Y as needed

            // Optional: customize appearance (font, size, color)
            fragment.TextState.Font = FontRepository.FindFont("Arial");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.Black;

            // Append the text fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified document (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Custom text inserted and saved to '{outputPath}'.");
    }
}