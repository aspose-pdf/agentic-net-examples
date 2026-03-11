using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class PdfSvgToMarkdown
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputMdPath = "output.md";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        var markdownBuilder = new StringBuilder();

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];
                // Extract SVG elements from the current page
                var svgExtractor = new SvgExtractor();
                List<string> svgContents = svgExtractor.Extract(page);

                if (svgContents.Count == 0)
                    continue; // No SVGs on this page, skip

                // Add a heading for the page in the markdown output
                markdownBuilder.AppendLine($"## Page {pageIndex}");
                markdownBuilder.AppendLine();

                // Process each extracted SVG
                foreach (string svg in svgContents)
                {
                    // Encode the SVG as Base64 for a data‑URI image
                    string base64Svg = Convert.ToBase64String(Encoding.UTF8.GetBytes(svg));
                    // Markdown image syntax using the data‑URI
                    markdownBuilder.AppendLine($"![Embedded SVG](data:image/svg+xml;base64,{base64Svg})");
                    markdownBuilder.AppendLine();
                }
            }
        }

        // Write the accumulated markdown to the output file
        File.WriteAllText(outputMdPath, markdownBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"Markdown file generated: {outputMdPath}");
    }
}