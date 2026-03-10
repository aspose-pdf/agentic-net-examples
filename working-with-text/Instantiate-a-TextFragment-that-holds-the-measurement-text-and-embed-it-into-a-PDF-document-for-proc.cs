using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string outputPath = "measurement.pdf";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Pages collection is 1‑based)
            Page page = doc.Pages.Add();

            // Create a TextFragment with the measurement text
            TextFragment tf = new TextFragment("Length: 10 cm");

            // Set the position where the text will appear on the page
            // Position uses Aspose.Pdf.Text.Position (XIndent, YIndent)
            tf.Position = new Position(100, 700);

            // Optional: set font, size, and color using the TextState
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Arial");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Append the TextFragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the PDF document (PDF format is the default)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with measurement text saved to '{outputPath}'.");
    }
}