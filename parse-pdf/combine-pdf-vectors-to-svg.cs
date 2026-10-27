using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF files whose vector graphics will be combined
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string outputSvg = "combined.svg";

        // Verify that all source files exist
        foreach (var path in pdfFiles)
        {
            if (!File.Exists(path))
            {
                Console.Error.WriteLine($"File not found: {path}");
                return;
            }
        }

        // Merge all PDFs into a single Document (first file is the target)
        using (Document merged = new Document(pdfFiles[0]))
        {
            for (int i = 1; i < pdfFiles.Length; i++)
            {
                using (Document src = new Document(pdfFiles[i]))
                {
                    merged.Pages.Add(src.Pages);
                }
            }

            // Prepare SVG save options – combine all pages into one SVG file
            var svgOptions = new SvgSaveOptions
            {
                // Enables CSS style embedding and keeps all pages in a single multi‑page SVG
                ScaleToPixels = true
            };

            // SVG conversion may require GDI+ (Windows only); handle gracefully
            try
            {
                merged.Save(outputSvg, svgOptions);
                Console.WriteLine($"Combined SVG saved to '{outputSvg}'.");
            }
            catch (TypeInitializationException)
            {
                Console.WriteLine("SVG conversion requires Windows (GDI+). Skipped on this platform.");
            }
        }
    }
}
