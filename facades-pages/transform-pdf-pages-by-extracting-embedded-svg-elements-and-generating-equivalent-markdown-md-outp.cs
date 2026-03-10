using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Vector;

class Program
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

        // Load the PDF document using Aspose.Pdf (core API) inside a using block for proper disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare a StringBuilder to accumulate markdown content.
            StringBuilder markdownBuilder = new StringBuilder();

            // Iterate through all pages (1‑based indexing).
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                Page page = pdfDoc.Pages[pageNumber];

                // Use SvgExtractor (Aspose.Pdf.Vector) to extract embedded SVG elements from the page.
                SvgExtractor svgExtractor = new SvgExtractor();
                List<string> svgContents = svgExtractor.Extract(page);

                // If the page contains SVG elements, convert each to a markdown image.
                if (svgContents != null && svgContents.Count > 0)
                {
                    markdownBuilder.AppendLine($"## Page {pageNumber}");
                    markdownBuilder.AppendLine();

                    foreach (string svg in svgContents)
                    {
                        // Encode the raw SVG as Base64 to embed it directly in markdown.
                        string base64Svg = Convert.ToBase64String(Encoding.UTF8.GetBytes(svg));

                        // Markdown image syntax with data URI.
                        markdownBuilder.AppendLine($"![Embedded SVG](data:image/svg+xml;base64,{base64Svg})");
                        markdownBuilder.AppendLine();
                    }
                }
            }

            // Write the accumulated markdown to the output file.
            File.WriteAllText(outputMdPath, markdownBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine($"Markdown file generated at '{outputMdPath}'.");
        }
    }
}