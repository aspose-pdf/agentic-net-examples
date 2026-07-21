using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Input PDF path, output PDF path and the text to add
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string textToAdd  = "Hello, Aspose.Pdf!";
        // Desired coordinates (X, Y) – measured from the bottom‑left corner of the page
        const double x = 100; // horizontal position
        const double y = 600; // vertical position

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The document does not contain any pages.");
                return;
            }

            // Retrieve the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextFragment with the desired string
            TextFragment fragment = new TextFragment(textToAdd);
            // Set the position where the text will be placed
            fragment.Position = new Position(x, y);
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

        Console.WriteLine($"Text added and saved to '{outputPath}'.");
    }
}