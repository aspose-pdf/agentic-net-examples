using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputSvg = "combined.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // SvgExtractor extracts vector graphics from a page as SVG strings
            SvgExtractor extractor = new SvgExtractor();

            StringBuilder sb = new StringBuilder();

            // Begin a single SVG wrapper that will contain all pages
            sb.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            sb.AppendLine(@"<svg xmlns=""http://www.w3.org/2000/svg"">");

            // Pages are 1‑based in Aspose.Pdf
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Extract all vector graphics on the current page
                var pageSvgs = extractor.Extract(page);

                // Group the graphics of each page under a <g> element
                sb.AppendLine($@"  <g id=""page{i}"">");

                foreach (var svgContent in pageSvgs)
                {
                    // Remove the outer <svg> element and XML declaration
                    // so the content can be embedded inside the wrapper
                    string inner = StripOuterSvg(svgContent);
                    sb.AppendLine(inner);
                }

                sb.AppendLine(@"  </g>");
            }

            // Close the wrapper SVG element
            sb.AppendLine(@"</svg>");

            // Write the combined SVG to disk
            File.WriteAllText(outputSvg, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"Combined SVG saved to '{outputSvg}'.");
        }
    }

    // Helper: removes the XML declaration and outer <svg> tags from an SVG string
    static string StripOuterSvg(string svg)
    {
        int start = svg.IndexOf("<svg", StringComparison.Ordinal);
        if (start < 0) return svg; // fallback if not found

        int startTagEnd = svg.IndexOf('>', start);
        if (startTagEnd < 0) return svg;

        int end = svg.LastIndexOf("</svg>", StringComparison.Ordinal);
        if (end < 0) return svg;

        return svg.Substring(startTagEnd + 1, end - (startTagEnd + 1)).Trim();
    }
}