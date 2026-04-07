using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_header.pdf";
        const string headerText = "Confidential Document";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Loop through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a HeaderFooter object for the current page
                HeaderFooter header = new HeaderFooter();

                // Build a TextFragment that represents the header text
                TextFragment tf = new TextFragment(headerText)
                {
                    // Position the text near the top of the page (Y coordinate is measured from bottom)
                    // Adjust the Y value as needed; here we use 20 points from the top edge.
                    Position = new Position(0, page.PageInfo.Height - 20),
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Set font and size via TextState (TextFragment does not expose Font/FontSize directly)
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.FontSize = 12;

                // Add the TextFragment to the header's paragraph collection
                header.Paragraphs.Add(tf);

                // Assign the prepared header to the page
                page.Header = header;
            }

            // Save the modified PDF (PDF format, no extra SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added to all pages. Saved as '{outputPath}'.");
    }
}
