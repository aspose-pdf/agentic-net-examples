using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Determine how many pages we can process (max 3)
            int pagesToProcess = Math.Min(3, doc.Pages.Count);

            // HTML content for the header (includes embedded CSS)
            string headerHtml = @"
                <style>
                    .myHeader {
                        font-family: Helvetica, Arial, sans-serif;
                        font-size: 14pt;
                        color: #0066CC;
                        text-align: center;
                        margin-top: 5pt;
                    }
                </style>
                <div class='myHeader'>Confidential Report – Page {page}</div>";

            // Apply the header to the first three pages
            for (int i = 1; i <= pagesToProcess; i++)
            {
                Page page = doc.Pages[i];

                // Create a new HeaderFooter object
                HeaderFooter header = new HeaderFooter();

                // Replace placeholder with actual page number
                string pageHeaderHtml = headerHtml.Replace("{page}", i.ToString());

                // Create an HtmlFragment containing the styled header
                HtmlFragment htmlFragment = new HtmlFragment(pageHeaderHtml);
                // No need to set IsHtml – HtmlFragment treats the string as HTML by default

                // Add the fragment to the header's paragraph collection
                header.Paragraphs.Add(htmlFragment);

                // Assign the header to the page
                page.Header = header;
            }

            // Save the modified document (PDF format)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HTML header on first three pages: '{outputPath}'");
    }
}
