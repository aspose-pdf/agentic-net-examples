using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class InsertCustomText
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the text will be placed (1‑based indexing)
            Page page = doc.Pages[1];

            // Create a TextFragment with the desired content
            TextFragment fragment = new TextFragment("Custom text with proper encoding");

            // Set the exact position on the page (coordinates are in points; origin is bottom‑left)
            fragment.Position = new Position(100, 700); // X = 100, Y = 700

            // Configure text appearance and encoding
            fragment.TextState.FontSize = 12;
            fragment.TextState.Font = FontRepository.FindFont("Arial Unicode MS"); // Unicode‑capable font
            fragment.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Use TextBuilder to append the fragment to the page
            TextBuilder builder = new TextBuilder(page);
            builder.AppendText(fragment);

            // Save the modified document (PDF format)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Custom text inserted and saved to '{outputPdf}'.");
    }
}