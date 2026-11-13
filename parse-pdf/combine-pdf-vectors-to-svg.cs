using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // List of PDF files whose vector graphics will be combined
        string[] pdfFiles = { "doc1.pdf", "doc2.pdf", "doc3.pdf" };
        string outputSvg = "combined.svg";

        // Ensure all input files exist before processing
        foreach (var file in pdfFiles)
        {
            if (!File.Exists(file))
            {
                Console.Error.WriteLine($"File not found: {file}");
                return;
            }
        }

        // Merge all PDFs into a single Document (target)
        using (Document target = new Document(pdfFiles[0]))
        {
            for (int i = 1; i < pdfFiles.Length; i++)
            {
                using (Document source = new Document(pdfFiles[i]))
                {
                    // Append pages from the source document to the target document
                    target.Pages.Add(source.Pages);
                }
            }

            // Configure SVG save options to produce one multi‑page SVG file
            SvgSaveOptions svgOptions = new SvgSaveOptions();
            // Optional: embed CSS styles by scaling to pixels (recommended for vector fidelity)
            svgOptions.ScaleToPixels = true;

            // Save the merged document as a single SVG document
            target.Save(outputSvg, svgOptions);
        }

        Console.WriteLine($"Combined SVG saved to '{outputSvg}'.");
    }
}
