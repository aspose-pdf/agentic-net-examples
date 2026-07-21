using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        // Input PDF files to process
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        // Output combined SVG file
        const string outputSvg = "combined.svg";

        // StringBuilder to accumulate SVG content from all pages
        StringBuilder combinedSvg = new StringBuilder();

        // Optional: add XML header for the combined SVG (single root element)
        combinedSvg.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
        combinedSvg.AppendLine(@"<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">");

        // Process each PDF document
        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document (using ensures proper disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Iterate through pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Skip pages without vector graphics
                    if (!page.HasVectorGraphics())
                        continue;

                    // Extract SVG strings from the page
                    SvgExtractor extractor = new SvgExtractor();
                    // This overload returns a list of SVG fragments (one per vector image)
                    var svgFragments = extractor.Extract(page);

                    // Append each fragment to the combined SVG
                    foreach (string fragment in svgFragments)
                    {
                        // Wrap each fragment in a <g> element with a comment indicating source
                        combinedSvg.AppendLine($"<!-- {Path.GetFileName(pdfPath)} - page {i} -->");
                        combinedSvg.AppendLine("<g>");
                        combinedSvg.AppendLine(fragment);
                        combinedSvg.AppendLine("</g>");
                    }
                }
            }
        }

        // Close the root <svg> element
        combinedSvg.AppendLine("</svg>");

        // Write the combined SVG content to the output file
        File.WriteAllText(outputSvg, combinedSvg.ToString(), Encoding.UTF8);
        Console.WriteLine($"Combined SVG saved to '{outputSvg}'.");
    }
}