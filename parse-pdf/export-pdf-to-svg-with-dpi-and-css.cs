using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputSvg = "output.svg";
        const string outputDir = "ExtractedSvgs";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Configure SVG save options
            SvgSaveOptions svgOptions = new SvgSaveOptions
            {
                // Scale typographic points to pixels (helps control DPI‑like output)
                ScaleToPixels = true,
                // Enable multi‑threading for faster processing
                IsMultiThreading = true
                // Uncomment to compress all SVG files into a zip archive
                // CompressOutputToZipArchive = true
            };

            // Save the entire document as a single SVG file
            pdfDoc.Save(outputSvg, svgOptions);
            Console.WriteLine($"Document saved as SVG: {outputSvg}");

            // Prepare extraction options for individual vector graphics
            SvgExtractionOptions extractOpts = new SvgExtractionOptions
            {
                // Increase minimum stroke width to improve visibility at higher DPI
                MinStrokeWidth = 0.8,
                // Allow partially visible elements to be extracted
                StrictExtractionAreaBoundCheck = false,
                // Group subpaths automatically where appropriate
                AutoGrouping = true
            };

            // Create an extractor with the custom options
            SvgExtractor extractor = new SvgExtractor(extractOpts);

            // Extract graphics from each page into separate SVG files
            Directory.CreateDirectory(outputDir);
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                string pageDir = Path.Combine(outputDir, $"Page_{i}");
                Directory.CreateDirectory(pageDir);
                extractor.Extract(page, pageDir);
                Console.WriteLine($"Extracted SVG graphics from page {i} to {pageDir}");
            }

            // Apply custom CSS styling to the extracted SVG files
            foreach (string svgFile in Directory.GetFiles(outputDir, "*.svg", SearchOption.AllDirectories))
            {
                string svgContent = File.ReadAllText(svgFile);
                // Simple CSS rule that sets a default fill color
                string css = "<style type=\"text/css\"> .custom-fill { fill:#ff0000; } </style>";
                int insertPos = svgContent.IndexOf('>');
                if (insertPos > 0)
                {
                    svgContent = svgContent.Insert(insertPos + 1, "\n" + css + "\n");
                    File.WriteAllText(svgFile, svgContent);
                }
            }
            Console.WriteLine("Custom CSS styling applied to extracted SVG files.");
        }
    }
}