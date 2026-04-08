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
        // Output combined SVG file
        const string outputSvg = "combined.svg";

        // StringBuilder to accumulate SVG content
        StringBuilder combinedSvg = new StringBuilder();

        // Start the outer SVG wrapper (basic header)
        combinedSvg.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
        combinedSvg.AppendLine(@"<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">");

        // Process each PDF file
        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document (lifecycle rule: use using)
            using (Document doc = new Document(pdfPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Use SvgExtractor to obtain SVG fragments for the current page
                    SvgExtractor extractor = new SvgExtractor();
                    List<string> pageSvgs = extractor.Extract(page);

                    // Wrap each page's graphics in a group for identification
                    combinedSvg.AppendLine($@"  <g id=""page_{i}"">");

                    foreach (string svgFragment in pageSvgs)
                    {
                        // The fragment usually contains its own <svg> root.
                        // For a simple merge we embed it as CDATA to keep it well‑formed.
                        combinedSvg.AppendLine(@"    <![CDATA[");
                        combinedSvg.AppendLine(svgFragment);
                        combinedSvg.AppendLine(@"    ]]>");
                    }

                    combinedSvg.AppendLine(@"  </g>");
                }
            }
        }

        // Close the outer SVG wrapper
        combinedSvg.AppendLine(@"</svg>");

        // Write the combined SVG to disk (lifecycle rule: use explicit save)
        File.WriteAllText(outputSvg, combinedSvg.ToString(), Encoding.UTF8);
        Console.WriteLine($"Combined SVG saved to '{outputSvg}'.");
    }
}