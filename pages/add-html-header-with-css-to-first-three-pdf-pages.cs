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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply header to the first three pages (1‑based indexing)
            for (int i = 1; i <= Math.Min(3, doc.Pages.Count); i++)
            {
                Page page = doc.Pages[i];

                // Create an HTML fragment with embedded CSS styling
                // Example: red background, white centered text, 14pt font
                string html = $@"<div style='background-color:#ff0000;color:#ffffff;
                                    text-align:center;font-size:14pt;
                                    padding:5px;'>
                                    Sample Header for Page {i}
                                </div>";

                // Use HtmlFragment (not TextFragment) for HTML content
                HtmlFragment htmlFragment = new HtmlFragment(html);

                // Build the header/footer object and add the fragment
                HeaderFooter header = new HeaderFooter();
                header.Paragraphs.Add(htmlFragment);

                // Assign the header to the page
                page.Header = header;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with headers to '{outputPath}'.");
    }
}
