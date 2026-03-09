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
        const string fontFamily = "Times New Roman";
        // Use a float literal (or cast) because TextState.FontSize expects a float
        const float fontSize = 12f;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Custom text inserted by Aspose.Pdf");
            // Set the position where the text will appear on the page
            tf.Position = new Position(100, 600);

            // Apply the specified font family and point size
            tf.TextState.Font = FontRepository.FindFont(fontFamily);
            tf.TextState.FontSize = fontSize; // now matches the expected float type
            // Optional: set text color (using Aspose.Pdf.Color)
            tf.TextState.ForegroundColor = Color.Black;

            // Use TextBuilder to append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
