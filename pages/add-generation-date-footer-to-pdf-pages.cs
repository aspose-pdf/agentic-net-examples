using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare the footer text (generation date)
            string footerText = DateTime.Now.ToString("yyyy-MM-dd");

            // Iterate over all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a HeaderFooter object if not already present
                if (page.Footer == null)
                    page.Footer = new HeaderFooter();

                // Clear any existing paragraphs (optional)
                page.Footer.Paragraphs.Clear();

                // Create a TextFragment with the date
                TextFragment tf = new TextFragment(footerText)
                {
                    // Position the footer near the bottom of the page
                    // (Y coordinate is measured from the bottom)
                    Position = new Position(0, 20) // X = 0 (left margin), Y = 20 points from bottom
                };

                // Set text appearance (optional)
                tf.TextState.FontSize = 10;
                tf.TextState.Font = FontRepository.FindFont("Helvetica");
                tf.TextState.ForegroundColor = Aspose.Pdf.Color.Gray;

                // Add the fragment to the footer's paragraph collection
                page.Footer.Paragraphs.Add(tf);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with footer: '{outputPath}'");
    }
}