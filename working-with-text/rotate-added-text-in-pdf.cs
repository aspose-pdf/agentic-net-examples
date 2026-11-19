using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_text.pdf";
        const string text = "Rotated Text Example";
        const double angleDegrees = 45; // rotation angle in degrees

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment(text);
            // Set the initial position of the text (optional)
            tf.Position = new Position(100, 500);

            // Rotate the text using TextState.Rotation (degrees)
            tf.TextState.Rotation = (float)angleDegrees;

            // Example: set text color using Aspose.Pdf.Color (cross‑platform)
            tf.TextState.ForegroundColor = Color.FromRgb(0, 0, 255); // blue

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved rotated text PDF to '{outputPath}'.");
    }
}
