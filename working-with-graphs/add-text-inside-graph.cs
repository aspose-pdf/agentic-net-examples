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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Assume the graph is on the first page; adjust the index if needed (1‑based)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment text = new TextFragment("Graph Annotation");

            // Set font family, size, and color
            text.TextState.Font = FontRepository.FindFont("Arial");          // Font family
            text.TextState.FontSize = 14;                                   // Font size
            text.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0, 0, 1); // Blue color

            // Position the text at specific coordinates (e.g., x=200, y=400)
            text.Position = new Position(200, 400);

            // Add the text fragment to the page's paragraph collection
            page.Paragraphs.Add(text);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text added and saved to '{outputPath}'.");
    }
}