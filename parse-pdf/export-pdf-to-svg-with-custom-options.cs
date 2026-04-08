using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Vector;

class SvgExportWithCustomOptions
{
    static void Main()
    {
        // Input PDF and output SVG paths
        const string pdfPath = "input.pdf";
        const string svgOutputPath = "output.svg";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document (using the standard lifecycle rule)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // -----------------------------------------------------------------
            // 1. Export the whole document to SVG using SvgSaveOptions.
            //    - ScaleToPixels converts typographic points to pixels (acts like DPI control).
            //    - IsMultiThreading enables parallel page processing for speed.
            // -----------------------------------------------------------------
            SvgSaveOptions svgSaveOpts = new SvgSaveOptions
            {
                ScaleToPixels = true,          // Treat points as pixels (approx. 96 DPI)
                IsMultiThreading = true       // Faster for multi‑page PDFs
                // Other options (CacheGlyphs, CloseResponse, etc.) can be set as needed.
            };

            // Save the PDF as SVG (one file per page will be created if the PDF has multiple pages)
            pdfDoc.Save(svgOutputPath, svgSaveOpts);
            Console.WriteLine($"Document saved as SVG to '{svgOutputPath}' using SvgSaveOptions.");

            // -----------------------------------------------------------------
            // 2. Extract vector graphics from a specific page with fine‑grained control.
            //    - Use SvgExtractionOptions to tweak stroke width, grouping, etc.
            //    - Then inject custom CSS into the resulting SVG string.
            // -----------------------------------------------------------------
            int pageNumber = 1; // 1‑based indexing
            if (pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine("Requested page number exceeds document page count.");
                return;
            }

            Page page = pdfDoc.Pages[pageNumber];

            // Configure extraction options
            SvgExtractionOptions extractionOpts = new SvgExtractionOptions
            {
                AutoGrouping = false,          // Disable automatic grouping of subpaths
                GroupStrength = 0.5,           // Moderate grouping strength
                MinStrokeWidth = 0.8,           // Ensure strokes thinner than 0.8 are widened
                StrictExtractionAreaBoundCheck = false,
                UnpackPageContentXForm = true
            };

            // Create the extractor with the above options
            SvgExtractor extractor = new SvgExtractor(extractionOpts);

            // Extract all vector graphics on the page as SVG strings
            var svgStrings = extractor.Extract(page);

            // Apply custom CSS styling to each SVG (e.g., set a background color)
            string customCss = @"
                .custom-bg { fill: #f0f0f0; }
                .custom-stroke { stroke: #ff0000; stroke-width: 2; }
            ";

            for (int i = 0; i < svgStrings.Count; i++)
            {
                string svg = svgStrings[i];

                // Insert a <style> block right after the opening <svg> tag
                int insertPos = svg.IndexOf('>') + 1;
                string styleBlock = $"<style type=\"text/css\"><![CDATA[{customCss}]]></style>";
                svg = svg.Insert(insertPos, styleBlock);

                // Optionally, add a class to the root <svg> element to apply the background
                svg = svg.Replace("<svg", "<svg class=\"custom-bg\"");

                // Save each modified SVG to a separate file
                string pageSvgPath = Path.Combine(
                    Path.GetDirectoryName(svgOutputPath) ?? ".",
                    $"page_{pageNumber}_vector_{i + 1}.svg");

                File.WriteAllText(pageSvgPath, svg, Encoding.UTF8);
                Console.WriteLine($"Vector graphic {i + 1} saved to '{pageSvgPath}'.");
            }
        }
    }
}