using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Process at most the first three pages (Aspose.Pdf uses 1‑based indexing)
            int pagesToProcess = Math.Min(3, doc.Pages.Count);

            // HTML header with embedded CSS styling
            string htmlHeader = @"
                <html>
                <head>
                    <style>
                        .myHeader {
                            font-family: Arial, Helvetica, sans-serif;
                            font-size: 24pt;
                            color: #003366;
                            text-align: center;
                            margin-bottom: 10pt;
                        }
                    </style>
                </head>
                <body>
                    <div class='myHeader'>Document Header</div>
                </body>
                </html>";

            // Add the HTML fragment to each of the first three pages
            for (int i = 1; i <= pagesToProcess; i++) // 1‑based page index
            {
                Page page = doc.Pages[i];
                HtmlFragment fragment = new HtmlFragment(htmlHeader);
                // Insert at the top of the page's content
                page.Paragraphs.Insert(0, fragment);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header added to first three pages. Saved as '{outputPath}'.");
    }
}