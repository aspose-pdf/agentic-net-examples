using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputSvg = "combined.svg";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle: load)
        using (Document pdfDoc = new Document(inputPdf))
        {
            StringBuilder sb = new StringBuilder();

            // Begin a single SVG document
            sb.AppendLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
            sb.AppendLine(@"<svg xmlns=""http://www.w3.org/2000/svg"" version=""1.1"">");

            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];

                // Skip pages without vector graphics
                if (!page.HasVectorGraphics())
                    continue;

                // Extract vector graphics of the current page to a temporary SVG file
                string tempPath = Path.GetTempFileName();
                bool extracted = page.TrySaveVectorGraphics(tempPath);

                if (extracted && File.Exists(tempPath))
                {
                    string pageSvg = File.ReadAllText(tempPath);

                    // Remove the outer <svg> wrapper, keep only inner content
                    int svgStart = pageSvg.IndexOf("<svg", StringComparison.Ordinal);
                    if (svgStart >= 0)
                    {
                        int contentStart = pageSvg.IndexOf('>', svgStart) + 1;
                        int contentEnd   = pageSvg.LastIndexOf("</svg>", StringComparison.Ordinal);
                        if (contentEnd > contentStart)
                        {
                            string innerContent = pageSvg.Substring(contentStart, contentEnd - contentStart);
                            sb.AppendLine($@"  <g id=""page{i}"">");
                            sb.AppendLine(innerContent);
                            sb.AppendLine("  </g>");
                        }
                    }
                    else
                    {
                        // Fallback: wrap whatever was returned
                        sb.AppendLine($@"  <g id=""page{i}"">{pageSvg}</g>");
                    }

                    // Delete the temporary file
                    File.Delete(tempPath);
                }
            }

            // Close the combined SVG document
            sb.AppendLine("</svg>");

            // Write the combined SVG to the target file (custom save, not a Document.Save)
            File.WriteAllText(outputSvg, sb.ToString());
            Console.WriteLine($"Combined SVG saved to '{outputSvg}'.");
        }
    }
}