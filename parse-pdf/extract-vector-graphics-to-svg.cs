using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedSvg";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate through each page (1‑based indexing)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    // Create a temporary document that contains only the current page
                    Document singlePageDoc = new Document();
                    // Import the page – this copies the page into the new document without removing it from the source
                    singlePageDoc.Pages.Add(pdfDoc.Pages[pageIndex]);

                    // Build a unique file name for the SVG output of this page
                    string svgFilePath = Path.Combine(outputFolder, $"Page_{pageIndex}.svg");

                    // Configure SVG save options – enable CSS style embedding via ScaleToPixels
                    SvgSaveOptions svgOptions = new SvgSaveOptions
                    {
                        ScaleToPixels = true
                    };

                    // Save the single‑page document as an SVG file
                    singlePageDoc.Save(svgFilePath, svgOptions);

                    Console.WriteLine($"Extracted SVG for page {pageIndex} -> {svgFilePath}");
                }
            }

            Console.WriteLine("Vector graphic extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
