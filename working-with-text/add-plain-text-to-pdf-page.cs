using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class AddPlainText
{
    static void Main()
    {
        // Input PDF path, output PDF path and text parameters
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string text       = "Hello, Aspose.Pdf!";
        const double xPos       = 100; // X coordinate
        const double yPos       = 600; // Y coordinate

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add text to the first page, and save
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextFragment with the desired string
            TextFragment tf = new TextFragment(text);

            // Set the position where the text will be placed
            tf.Position = new Position(xPos, yPos);

            // Optionally customize appearance (font, size, color)
            // tf.TextState.Font = FontRepository.FindFont("Helvetica");
            // tf.TextState.FontSize = 12;
            // tf.TextState.ForegroundColor = Color.Black;

            // Append the fragment to the page using TextBuilder
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(tf);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text added and saved to '{outputPath}'.");
    }
}