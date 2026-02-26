using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // for XFDF import/export methods
using Aspose.Pdf.Vector;        // for SVG extraction if needed (not used directly here)

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string xfdfPath       = "annotations.xfdf";
        const string svgOutputDir   = "svg_pages";
        const string finalPdfPath   = "final.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the directory for SVG pages exists
        Directory.CreateDirectory(svgOutputDir);

        // Load the PDF, export its annotations, convert pages to SVG,
        // import the annotations back, and save the final PDF.
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ----- Export annotations to XFDF -----
            pdfDoc.ExportAnnotationsToXfdf(xfdfPath);
            Console.WriteLine($"Annotations exported to '{xfdfPath}'.");

            // ----- Convert each page to a separate SVG file -----
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                string svgPath = Path.Combine(svgOutputDir, $"page_{i}.svg");

                // Create a temporary document containing only the current page
                using (Document singlePageDoc = new Document())
                {
                    // Add the page (pages are 1‑based)
                    singlePageDoc.Pages.Add(pdfDoc.Pages[i]);

                    // Save the single‑page document as SVG
                    SvgSaveOptions svgOpts = new SvgSaveOptions();
                    singlePageDoc.Save(svgPath, svgOpts);
                }

                Console.WriteLine($"Page {i} saved as SVG: '{svgPath}'.");
            }

            // ----- Import annotations back from XFDF (round‑trip) -----
            pdfDoc.ImportAnnotationsFromXfdf(xfdfPath);
            Console.WriteLine($"Annotations imported from '{xfdfPath}'.");

            // ----- Save the final PDF -----
            pdfDoc.Save(finalPdfPath);
            Console.WriteLine($"Final PDF saved to '{finalPdfPath}'.");
        }
    }
}