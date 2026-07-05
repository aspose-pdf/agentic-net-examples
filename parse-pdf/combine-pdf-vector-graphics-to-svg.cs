using System;
using System.IO;
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
        const string outputSvg = "combined.svg";

        // Collect SVG fragments from all pages of all PDFs
        List<string> svgFragments = new List<string>();

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load PDF document (lifecycle rule: use using for disposal)
            using (Document doc = new Document(pdfPath))
            {
                // Iterate through pages (1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Extract vector graphics from the page
                    SvgExtractor extractor = new SvgExtractor();
                    List<string> pageSvgs = extractor.Extract(page); // returns SVG strings

                    // If the page contains vector graphics, combine them
                    if (pageSvgs != null && pageSvgs.Count > 0)
                    {
                        // Concatenate all SVG strings for this page
                        string combinedPageSvg = string.Join("\n", pageSvgs);
                        svgFragments.Add(combinedPageSvg);
                    }
                }
            }
        }

        // Write a single SVG file that contains all extracted fragments
        using (StreamWriter writer = new StreamWriter(outputSvg))
        {
            writer.WriteLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            writer.WriteLine(@"<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">");

            // Insert each page's SVG content
            foreach (string fragment in svgFragments)
            {
                writer.WriteLine(fragment);
            }

            writer.WriteLine(@"</svg>");
        }

        Console.WriteLine($"Combined SVG saved to '{outputSvg}'.");
    }
}