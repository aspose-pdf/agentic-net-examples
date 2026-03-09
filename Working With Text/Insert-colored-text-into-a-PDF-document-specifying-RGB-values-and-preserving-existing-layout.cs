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

        // Load the existing PDF; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Select the page where the text will be inserted (first page in this example)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment tf = new TextFragment("Colored Text Example");

            // Position the fragment (adjust coordinates as needed to preserve layout)
            tf.Position = new Position(100, 700);

            // Optional: set font size
            tf.TextState.FontSize = 12;

            // Set the foreground color using RGB values (e.g., orange)
            tf.TextState.ForegroundColor = Aspose.Pdf.Color.FromArgb(255, 165, 0); // RGB(255,165,0)

            // Append the text fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified PDF (document-disposal-with-using rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with colored text to '{outputPath}'.");
    }
}