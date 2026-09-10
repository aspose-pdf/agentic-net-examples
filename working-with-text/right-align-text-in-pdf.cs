using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "right_aligned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create a text fragment with the desired content
            TextFragment textFragment = new TextFragment("Right aligned text");

            // Set the horizontal alignment to Right using the TextState property
            textFragment.TextState.HorizontalAlignment = HorizontalAlignment.Right;

            // Position can be set; alignment will be applied relative to the page width
            textFragment.Position = new Position(0, 0);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(textFragment);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Right‑aligned PDF saved to '{outputPath}'.");
    }
}