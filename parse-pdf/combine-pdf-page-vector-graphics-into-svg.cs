using System;
using System.IO;
using System.Collections.Generic;
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

        // Load the PDF document (lifecycle: create → load → use → dispose)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // SvgExtractor extracts vector graphics from a page.
            SvgExtractor extractor = new SvgExtractor();

            // Collect SVG fragments for each page.
            List<string> pageSvgs = new List<string>();

            // Pages are 1‑based.
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Extract vector graphics as SVG strings.
                // A page may contain multiple separate SVG images, so we get a list.
                List<string> svgs = extractor.Extract(page);

                if (svgs.Count > 0)
                {
                    // Concatenate all SVG fragments of the page.
                    string combinedPageSvg = string.Join(Environment.NewLine, svgs);

                    // Wrap the page's graphics in a <g> element for identification.
                    string wrapped = $"<g id=\"page{i}\">{combinedPageSvg}</g>";
                    pageSvgs.Add(wrapped);
                }
            }

            // Build a single multi‑page SVG document.
            string svgHeader = @"<?xml version=""1.0"" encoding=""UTF-8""?>"
                             + Environment.NewLine
                             + @"<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">";

            string svgFooter = "</svg>";

            string finalSvg = svgHeader + Environment.NewLine
                            + string.Join(Environment.NewLine, pageSvgs) + Environment.NewLine
                            + svgFooter;

            // Save the combined SVG file.
            File.WriteAllText(outputSvg, finalSvg);
        }

        Console.WriteLine($"Combined SVG saved to '{outputSvg}'.");
    }
}