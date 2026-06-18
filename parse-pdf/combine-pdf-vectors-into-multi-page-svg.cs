using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Output multi‑page SVG file
        const string outputSvg = "combined_vectors.svg";

        // StringBuilder to accumulate SVG content
        StringBuilder svgBuilder = new StringBuilder();

        // SVG header
        svgBuilder.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
        svgBuilder.AppendLine(@"<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">");

        int pageCounter = 1;

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load each PDF inside a using block (lifecycle rule)
            using (Document doc = new Document(pdfPath))
            {
                // Iterate through all pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Skip pages without vector graphics
                    if (!page.HasVectorGraphics())
                        continue;

                    // Extract vector graphics from the page as SVG strings
                    SvgExtractor extractor = new SvgExtractor();
                    List<string> pageSvgs = extractor.Extract(page); // returns List<string>

                    // Wrap each page's graphics in a group for identification
                    svgBuilder.AppendLine($@"  <g id=""page_{pageCounter}"">");

                    foreach (string svgFragment in pageSvgs)
                    {
                        // The fragment is a complete <svg> element; embed its inner content.
                        // Simple approach: strip outer <svg> tags if present.
                        string inner = StripOuterSvgTag(svgFragment);
                        svgBuilder.AppendLine(inner);
                    }

                    svgBuilder.AppendLine("  </g>");
                    pageCounter++;
                }
            }
        }

        // Close the root SVG element
        svgBuilder.AppendLine("</svg>");

        // Write the combined SVG to disk
        File.WriteAllText(outputSvg, svgBuilder.ToString(), Encoding.UTF8);
        Console.WriteLine($"Combined SVG saved to '{outputSvg}'.");
    }

    // Helper: removes the outer <svg>...</svg> wrapper, preserving inner markup.
    static string StripOuterSvgTag(string svg)
    {
        if (string.IsNullOrEmpty(svg))
            return string.Empty;

        int start = svg.IndexOf('>'); // after opening tag
        int end   = svg.LastIndexOf("</svg>", StringComparison.OrdinalIgnoreCase);
        if (start < 0 || end < 0 || end <= start)
            return svg; // fallback: return as‑is

        return svg.Substring(start + 1, end - start - 1).Trim();
    }
}