using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least two pages
            if (doc.Pages.Count < 2)
            {
                Console.Error.WriteLine("The document does not contain a second page.");
                return;
            }

            // Get page two (1‑based indexing)
            Page pageTwo = doc.Pages[2];

            // Create a text fragment with the desired content
            TextFragment fragment = new TextFragment("Centered content on page two");

            // Set horizontal alignment to Center
            fragment.TextState.HorizontalAlignment = HorizontalAlignment.Center;

            // Optionally set font size and color for better visibility
            fragment.TextState.FontSize = 14;
            fragment.TextState.ForegroundColor = Color.Black;

            // Add the fragment to the page's paragraph collection
            pageTwo.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with centered content on page two: {outputPath}");
    }
}