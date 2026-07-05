using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // needed for FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add headers, and save
        using (Document doc = new Document(inputPath))
        {
            // Prepare a bold font for the header
            Font boldFont = FontRepository.FindFont("Helvetica-Bold");

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a TextFragment that will act as the header
                TextFragment headerFragment = new TextFragment("SECTION HEADING")
                {
                    // Position the header a little below the top edge (Y coordinate is from bottom)
                    // Aspose.Pdf uses the bottom‑left as origin, so we calculate Y = page height - margin
                    Position = new Position(0, page.PageInfo.Height - 20)
                };

                // Configure the appearance of the header
                headerFragment.TextState.Font = boldFont;          // bold typeface
                headerFragment.TextState.FontSize = 12;            // reasonable size
                headerFragment.TextState.FontStyle = FontStyles.Bold;
                headerFragment.TextState.ForegroundColor = Color.Black;
                headerFragment.HorizontalAlignment = HorizontalAlignment.Center;

                // Add the header to the page's paragraphs collection
                page.Paragraphs.Add(headerFragment);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with headers: {outputPath}");
    }
}
