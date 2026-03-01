using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Text;                // TextFragment, Position, etc.

class Program
{
    static void Main()
    {
        // Output PDF file path
        const string outputPath = "created_document.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Hello, Aspose.Pdf!");

            // Optionally set font, size and color (using Aspose.Pdf.Color)
            tf.TextState.Font = FontRepository.FindFont("Helvetica");
            tf.TextState.FontSize = 14;
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Position the text on the page (coordinates are in points, origin at bottom‑left)
            tf.Position = new Position(100, 700);

            // Add the text fragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Save the document as a PDF file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF document created at '{outputPath}'.");
    }
}