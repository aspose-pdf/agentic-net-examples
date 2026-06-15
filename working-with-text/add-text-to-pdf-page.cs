using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class AddTextExample
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing; get the first page
            Page page = doc.Pages[1];

            // Create a TextFragment with the desired plain text
            TextFragment fragment = new TextFragment("Hello World");

            // Set the position where the text will be placed (X, Y)
            fragment.Position = new Position(100, 600);

            // Optional: customize appearance (font, size, color)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.Black;

            // Use TextBuilder to append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text added and PDF saved to '{outputPath}'.");
    }
}