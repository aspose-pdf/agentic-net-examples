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
        // Input PDF files – adjust paths as needed
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Output combined SVG file
        const string outputSvg = "combined.svg";

        // StringBuilder to accumulate SVG content
        StringBuilder combinedSvg = new StringBuilder();

        // Root SVG element – will contain all extracted graphics
        combinedSvg.AppendLine("<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\">");

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load PDF document inside a using block (lifecycle rule)
            using (Document doc = new Document(pdfPath))
            {
                // Iterate pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Use SvgExtractor to get SVG strings for the page
                    SvgExtractor extractor = new SvgExtractor();
                    List<string> pageSvgs = extractor.Extract(page);

                    // If the page contains vector graphics, embed them
                    if (pageSvgs != null && pageSvgs.Count > 0)
                    {
                        // Wrap each extracted SVG in a <g> element with a unique id
                        int graphicIndex = 1;
                        foreach (string svgContent in pageSvgs)
                        {
                            // Remove outer <svg> tags to embed the inner content
                            string inner = ExtractInnerSvg(svgContent);
                            combinedSvg.AppendLine($"  <g id=\"pdf{Path.GetFileNameWithoutExtension(pdfPath)}_page{pageIndex}_g{graphicIndex}\">");
                            combinedSvg.AppendLine(inner);
                            combinedSvg.AppendLine("  </g>");
                            graphicIndex++;
                        }
                    }
                }
            }
        }

        // Close root SVG element
        combinedSvg.AppendLine("</svg>");

        // Write the combined SVG to disk
        File.WriteAllText(outputSvg, combinedSvg.ToString());
        Console.WriteLine($"Combined SVG saved to '{outputSvg}'.");
    }

    // Helper: strips the outer <svg>…</svg> tags, returning only the inner markup
    static string ExtractInnerSvg(string svg)
    {
        if (string.IsNullOrEmpty(svg))
            return string.Empty;

        int start = svg.IndexOf('>');
        if (start < 0) return svg; // malformed – return as‑is
        start++; // move past '>'

        int end = svg.LastIndexOf("</svg>", StringComparison.OrdinalIgnoreCase);
        if (end < 0) end = svg.Length;

        return svg.Substring(start, end - start).Trim();
    }
}