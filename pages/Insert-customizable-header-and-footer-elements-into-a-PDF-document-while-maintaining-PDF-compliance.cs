using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_header_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Prepare reusable header and footer templates
            // -----------------------------------------------------------------
            HeaderFooter headerTemplate = new HeaderFooter();
            // Add a simple text fragment to the header
            TextFragment headerText = new TextFragment("My Document Header");
            headerText.TextState.FontSize = 12;
            headerText.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;
            headerTemplate.Paragraphs.Add(headerText);
            // Optional margin for the header (distance from the top edge)
            headerTemplate.Margin = new MarginInfo { Top = 10, Bottom = 0, Left = 0, Right = 0 };

            HeaderFooter footerTemplate = new HeaderFooter();
            // Footer will contain a static label; the page number will be appended later
            TextFragment footerLabel = new TextFragment("Page ");
            footerLabel.TextState.FontSize = 10;
            footerLabel.TextState.ForegroundColor = Aspose.Pdf.Color.DarkGray;
            footerTemplate.Paragraphs.Add(footerLabel);
            // Optional margin for the footer (distance from the bottom edge)
            footerTemplate.Margin = new MarginInfo { Top = 0, Bottom = 10, Left = 0, Right = 0 };

            // -----------------------------------------------------------------
            // Apply header/footer to each page. The OnBeforePageGenerate event does not
            // exist in recent Aspose.Pdf versions, so we iterate over the pages directly.
            // -----------------------------------------------------------------
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Clone the templates so each page gets its own objects
                HeaderFooter pageHeader = (HeaderFooter)headerTemplate.Clone();
                HeaderFooter pageFooter = (HeaderFooter)footerTemplate.Clone();

                // Append the current page number to the footer
                TextFragment pageNumber = new TextFragment(page.Number.ToString());
                pageNumber.TextState.FontSize = 10;
                pageNumber.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                pageFooter.Paragraphs.Add(pageNumber);

                // Assign the prepared header and footer to the page
                page.Header = pageHeader;
                page.Footer = pageFooter;
            }

            // -----------------------------------------------------------------
            // Save the modified document. Header/footer have already been attached
            // to each page, preserving PDF compliance.
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with header and footer: {outputPath}");
    }
}
