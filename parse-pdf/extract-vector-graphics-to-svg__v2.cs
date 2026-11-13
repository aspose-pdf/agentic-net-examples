using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Path to the source PDF
        const string inputPath = "input.pdf";

        // Page number to extract vector graphics from (1‑based indexing)
        const int pageNumber = 2;

        // Directory where the SVG files will be saved
        const string outputDir = "VectorGraphics";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the source PDF
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
            {
                // Validate requested page number
                if (pageNumber < 1 || pageNumber > doc.Pages.Count)
                {
                    Console.Error.WriteLine("Invalid page number.");
                    return;
                }

                // Create a new temporary document that will contain only the target page
                using (Aspose.Pdf.Document singlePageDoc = new Aspose.Pdf.Document())
                {
                    // Add (clone) the selected page to the temporary document
                    singlePageDoc.Pages.Add(doc.Pages[pageNumber]);

                    // Prepare SVG save options (all vector graphics are preserved by default)
                    Aspose.Pdf.SvgSaveOptions svgOptions = new Aspose.Pdf.SvgSaveOptions();

                    // Build output file name
                    string svgPath = Path.Combine(outputDir, $"Page_{pageNumber}_vector.svg");

                    // Save the single‑page document as SVG
                    singlePageDoc.Save(svgPath, svgOptions);

                    Console.WriteLine($"Vector graphics extracted to: {svgPath}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}