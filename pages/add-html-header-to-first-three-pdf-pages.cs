using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

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

            // Loop through the first three pages (1‑based indexing)
            for (int i = 1; i <= pagesToProcess; i++)
            {
                Page page = doc.Pages[i];

                // Create a new header/footer object
                HeaderFooter header = new HeaderFooter();

                // HTML string with embedded CSS styling
                string html = $@"
                    <div style='font-family:Helvetica; font-size:12pt; color:#FF0000;'>
                        Header for page {i}
                    </div>";

                // HtmlFragment renders the HTML inside the PDF
                HtmlFragment htmlFragment = new HtmlFragment(html);

                // Add the HTML fragment to the header's paragraph collection
                header.Paragraphs.Add(htmlFragment);

                // Assign the header to the current page
                page.Header = header;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with HTML headers saved to '{outputPath}'.");
    }
}