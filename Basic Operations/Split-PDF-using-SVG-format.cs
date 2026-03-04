using System;
using System.IO;
using Aspose.Pdf;               // Core Aspose.Pdf namespace (contains Document, SvgSaveOptions, etc.)

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdfPath = "input.pdf";

        // Output directory where individual SVG files will be placed
        const string outputDir = "output_svg";

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Build a dummy SVG file name; when TreatTargetFileNameAsDirectory is true,
        // Aspose.Pdf will create a sub‑folder with this name and place each page's SVG inside it.
        string dummySvgPath = Path.Combine(outputDir, "page.svg");

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Configure SVG save options
                SvgSaveOptions svgOptions = new SvgSaveOptions
                {
                    // Create a dedicated folder for the SVG pages
                    TreatTargetFileNameAsDirectory = true,

                    // Optional: improve performance for large documents
                    IsMultiThreading = true
                };

                // Save the PDF as SVG; multiple pages will be written as separate SVG files
                pdfDoc.Save(dummySvgPath, svgOptions);
            }

            Console.WriteLine($"PDF split into SVG pages under '{outputDir}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}