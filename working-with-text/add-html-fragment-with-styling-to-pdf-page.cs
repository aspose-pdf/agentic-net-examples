using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
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

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Create an HTML fragment with styled markup
            HtmlFragment htmlFragment = new HtmlFragment("<b>Hello <i>World</i></b>");

            // Optional: customize the appearance via TextState
            // Set font, size and color using Aspose.Pdf types (cross‑platform)
            htmlFragment.TextState.Font = FontRepository.FindFont("Helvetica");
            htmlFragment.TextState.FontSize = 14;
            htmlFragment.TextState.ForegroundColor = Aspose.Pdf.Color.Blue;

            // Add the HTML fragment to the page's paragraph collection
            page.Paragraphs.Add(htmlFragment);

            // Save the modified PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"HTML fragment added and saved to '{outputPath}'.");
    }
}