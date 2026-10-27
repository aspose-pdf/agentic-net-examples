using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // required for HtmlFragment

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Define the HTML header with embedded CSS.
            string htmlHeader = @"
                <html>
                <head>
                    <style>
                        .header {
                            font-size:14pt;
                            color:#333333;
                            text-align:center;
                            margin-bottom:10pt;
                        }
                    </style>
                </head>
                <body>
                    <div class='header'>My Document Header</div>
                </body>
                </html>";

            // Add the header to the first three pages (or fewer if the document has less pages).
            int pagesToProcess = Math.Min(3, doc.Pages.Count);
            for (int i = 1; i <= pagesToProcess; i++) // Aspose.Pdf uses 1‑based page indexing
            {
                Page page = doc.Pages[i];
                // Create a fresh HtmlFragment for each page (a fragment instance cannot be shared across pages).
                HtmlFragment headerFragment = new HtmlFragment(htmlHeader);
                // Insert the header at the beginning of the page's paragraph collection.
                page.Paragraphs.Insert(0, headerFragment);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with HTML header to '{outputPath}'.");
    }
}
