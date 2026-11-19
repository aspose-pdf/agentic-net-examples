using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the text will be placed
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(50, 750, 550, 50);

            // Create a TextFragment containing multiple lines
            TextFragment tf = new TextFragment("Line 1\nLine 2\nLine 3");

            // Set the line spacing (leading) in points using the correct property
            tf.TextState.LineSpacing = 20; // 20‑point spacing between baselines

            // Optional: set additional text properties
            tf.TextState.FontSize = 12;
            tf.TextState.Font = FontRepository.FindFont("Arial");

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(tf);

            // Save the modified PDF; Document.Save writes PDF by default
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
