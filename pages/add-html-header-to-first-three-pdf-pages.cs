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

        // Load the source PDF inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // Define the HTML header with embedded CSS styling
            string headerHtml = @"
                <html>
                <head>
                    <style>
                        .myHeader { 
                            font-family: Arial, Helvetica, sans-serif; 
                            font-size: 14pt; 
                            color: #003366; 
                            text-align: center; 
                            margin: 5pt 0; 
                        }
                    </style>
                </head>
                <body>
                    <div class='myHeader'>Confidential Report – Page {pageNumber}</div>
                </body>
                </html>";

            // Apply the header to the first three pages (1‑based indexing)
            for (int i = 1; i <= Math.Min(3, doc.Pages.Count); i++)
            {
                Page page = doc.Pages[i];

                // Create a new HeaderFooter object
                HeaderFooter header = new HeaderFooter();

                // HtmlFragment parses the HTML string; placeholders like {pageNumber}
                // are not automatically replaced, so we inject the actual page number.
                string pageHeaderHtml = headerHtml.Replace("{pageNumber}", i.ToString());

                // Add the HTML fragment to the header
                header.Paragraphs.Add(new HtmlFragment(pageHeaderHtml));

                // Assign the header to the page
                page.Header = header;
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HTML header on first three pages: {outputPath}");
    }
}