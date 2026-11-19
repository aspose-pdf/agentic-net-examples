using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for TextFragment and TextState

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

        // Load the existing PDF (or create a new one if needed)
        using (Document doc = new Document(inputPath))
        {
            // Create a new text fragment
            TextFragment tf = new TextFragment("Line 1\nLine 2\nLine 3");

            // Set a custom line spacing (e.g., 1.8 times the default font size)
            // TextFragmentState does not expose a LineHeight property; use LineSpacing instead.
            tf.TextState.LineSpacing = 1.8f; // float value representing line spacing multiplier

            // Optional: set other text properties
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Arial");
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Add the fragment to the first page
            Page page = doc.Pages[1];
            page.Paragraphs.Add(tf);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with custom line spacing to '{outputPath}'.");
    }
}
