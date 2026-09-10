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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the existing PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Aspose.Pdf.Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            Aspose.Pdf.Text.TextFragment textFragment = new Aspose.Pdf.Text.TextFragment("Rotated Text");

            // Set the baseline position where the text will be placed
            textFragment.Position = new Aspose.Pdf.Text.Position(100, 500);

            // Rotate the text by specifying an angle (in degrees) via the TextState
            textFragment.TextState.Rotation = 45; // 45° clockwise rotation

            // Optional visual styling
            textFragment.TextState.FontSize = 24;
            textFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the text fragment to the page's paragraph collection
            page.Paragraphs.Add(textFragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}