using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";   // existing PDF to host the HTML fragment
        const string outputPdf = "styled_output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF (or create a new one if you prefer)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure we work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define CSS rules and HTML content inside a single HtmlFragment
            string html = @"
                <html>
                <head>
                    <style>
                        body {
                            font-family: Arial, Helvetica, sans-serif;
                            color: #2A2A2A;
                            margin: 20px;
                            line-height: 1.6;
                        }
                        h1 {
                            font-size: 24pt;
                            margin-bottom: 12px;
                        }
                        p {
                            margin-top: 8px;
                            margin-bottom: 8px;
                        }
                    </style>
                </head>
                <body>
                    <h1>Sample Heading</h1>
                    <p>This paragraph demonstrates embedded CSS controlling font, color, and spacing.</p>
                    <p>Another paragraph to show line‑height and margin effects.</p>
                </body>
                </html>";

            // Create the HtmlFragment with the styled HTML
            HtmlFragment fragment = new HtmlFragment(html);

            // Add the fragment to the page's paragraph collection
            page.Paragraphs.Add(fragment);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with embedded CSS saved to '{outputPdf}'.");
    }
}