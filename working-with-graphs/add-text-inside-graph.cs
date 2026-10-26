using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // PDF that already contains a graph
        const string outputPath = "output.pdf";     // Resulting PDF with added text

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page that contains the graph (first page in this example)
            Page page = doc.Pages[1];   // 1‑based indexing

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Sample Text");

            // Set font family, size and color
            tf.TextState.Font = FontRepository.FindFont("Arial");   // Font family
            tf.TextState.FontSize = 14;                             // Font size
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(0.0, 0.5, 0.8); // Color (RGB)

            // Position the text at specific coordinates (e.g., x=150, y=300)
            tf.Position = new Position(150, 300); // Coordinates are in points

            // Add the text fragment to the page's paragraphs collection
            page.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text added and saved to '{outputPath}'.");
    }
}