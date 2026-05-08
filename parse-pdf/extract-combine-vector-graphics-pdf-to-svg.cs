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
        const string inputPdfPath  = "input.pdf";
        const string outputSvgPath = "combined_vectors.svg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document (lifecycle: using block ensures disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // SvgExtractor extracts vector graphics from a page
            SvgExtractor extractor = new SvgExtractor();

            // StringBuilder will hold the combined SVG content
            StringBuilder combinedSvg = new StringBuilder();

            // Optional XML header for a well‑formed SVG file
            combinedSvg.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");

            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Extract all SVG strings representing vector graphics on this page
                List<string> pageSvgs = extractor.Extract(page);

                // If the page contains vector graphics, wrap them in a container <svg>
                if (pageSvgs.Count > 0)
                {
                    // Create a wrapper <svg> element matching the page size
                    combinedSvg.AppendLine($@"<svg width=""{page.PageInfo.Width}"" height=""{page.PageInfo.Height}"" xmlns=""http://www.w3.org/2000/svg"">");

                    // Append each extracted SVG fragment (they already contain their own <svg> tags,
                    // so we strip the outer <svg> element to avoid nesting)
                    foreach (string svgFragment in pageSvgs)
                    {
                        // Simple approach: locate the first '>' after the opening <svg ...> and the last '</svg>'
                        int start = svgFragment.IndexOf('>') + 1;
                        int end   = svgFragment.LastIndexOf("</svg>", StringComparison.Ordinal);
                        if (start > 0 && end > start)
                        {
                            string innerContent = svgFragment.Substring(start, end - start);
                            combinedSvg.AppendLine(innerContent);
                        }
                        else
                        {
                            // Fallback – append the whole fragment if parsing fails
                            combinedSvg.AppendLine(svgFragment);
                        }
                    }

                    combinedSvg.AppendLine("</svg>");
                }
            }

            // Write the combined SVG content to the output file
            File.WriteAllText(outputSvgPath, combinedSvg.ToString(), Encoding.UTF8);
            Console.WriteLine($"Combined vector graphics saved to '{outputSvgPath}'.");
        }
    }
}