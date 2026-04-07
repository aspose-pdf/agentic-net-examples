using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class AddPlainText
{
    static void Main()
    {
        // Input PDF file (must exist)
        const string inputPath  = "input.pdf";
        // Output PDF file
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Create a TextFragment with the desired plain text
            TextFragment fragment = new TextFragment("Hello, Aspose.Pdf!");

            // Set the position where the text will be placed (X, Y coordinates)
            fragment.Position = new Position(100, 600); // adjust as needed

            // Optional: customize appearance (font, size, color)
            fragment.TextState.Font = FontRepository.FindFont("Helvetica");
            fragment.TextState.FontSize = 12;
            fragment.TextState.ForegroundColor = Color.Blue;

            // Use TextBuilder to append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified document as PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Text added and saved to '{outputPath}'.");
    }
}