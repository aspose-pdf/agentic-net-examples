using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    // Removes the outer <svg>...</svg> tags from an SVG string.
    static string StripOuterSvg(string svgContent)
    {
        if (string.IsNullOrEmpty(svgContent))
            return string.Empty;

        int start = svgContent.IndexOf('>'); // end of opening <svg ...>
        int end   = svgContent.LastIndexOf("</svg>", StringComparison.Ordinal);
        if (start < 0 || end < 0 || end <= start)
            return svgContent; // fallback: return as‑is

        return svgContent.Substring(start + 1, end - start - 1).Trim();
    }

    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "combined_output.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Prepare a StringBuilder to hold the combined SVG.
            StringBuilder combinedSvg = new StringBuilder();

            // Start the root <svg> element. Width/height are optional; we use viewBox to encompass all pages.
            combinedSvg.AppendLine("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\">");

            // Iterate over all pages (1‑based indexing).
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // Use SvgExtractor to obtain all vector graphics on the current page as SVG strings.
                SvgExtractor extractor = new SvgExtractor();
                List<string> pageSvgs = extractor.Extract(page);

                // If the page contains vector graphics, wrap each SVG fragment in a <g> element.
                for (int i = 0; i < pageSvgs.Count; i++)
                {
                    string innerSvg = StripOuterSvg(pageSvgs[i]);
                    combinedSvg.AppendLine($"  <g id=\"page{pageIndex}_graphic{i + 1}\">");
                    combinedSvg.AppendLine(innerSvg);
                    combinedSvg.AppendLine("  </g>");
                }
            }

            // Close the root <svg> element.
            combinedSvg.AppendLine("</svg>");

            // Write the combined SVG content to the output file.
            File.WriteAllText(outputSvgPath, combinedSvg.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"Combined SVG saved to '{outputSvgPath}'.");
    }
}