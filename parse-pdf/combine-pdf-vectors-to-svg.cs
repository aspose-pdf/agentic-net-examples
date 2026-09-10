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
        // Output SVG file that will contain all extracted vector graphics
        const string outputSvg = "combined_vectors.svg";

        StringBuilder sb = new StringBuilder();

        // Iterate over each PDF document
        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load the PDF document (lifecycle rule: use Document constructor with file path)
            using (Document doc = new Document(pdfPath))
            {
                // Process each page (Aspose.Pdf uses 1‑based indexing)
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Check if the page contains vector graphics
                    if (!page.HasVectorGraphics())
                        continue;

                    // Extract SVG strings from the page
                    SvgExtractor extractor = new SvgExtractor();
                    // Extract returns a list of SVG strings (one per vector image on the page)
                    var svgList = extractor.Extract(page);

                    // Append each SVG to the combined output, with simple markers
                    foreach (string svgContent in svgList)
                    {
                        sb.AppendLine($"<!-- Source: {Path.GetFileName(pdfPath)} Page: {i} -->");
                        sb.AppendLine(svgContent);
                        sb.AppendLine(); // separate entries
                    }
                }
            }
        }

        // Write the combined SVG content to the output file
        try
        {
            File.WriteAllText(outputSvg, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"Combined SVG saved to '{outputSvg}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to write SVG file: {ex.Message}");
        }
    }
}