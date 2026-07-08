using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // FontRepository & SimpleFontSubstitution

class PdfPagesToBmp
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputDir = "BmpPages";          // folder for BMP images

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Verify that the source PDF exists before proceeding
        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Input file '{inputPdf}' not found. Operation aborted.");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Apply font substitution: replace Helvetica with Arial
            FontRepository.Substitutions.Add(new SimpleFontSubstitution("Helvetica", "Arial"));

            // Initialize PdfConverter facade with the document
            using (PdfConverter converter = new PdfConverter(doc))
            {
                // Convert only pages 5 through 7 (1‑based indexing)
                converter.StartPage = 5;
                converter.EndPage   = 7;

                // Prepare the converter
                converter.DoConvert();

                int pageIndex = converter.StartPage; // will be 5

                // Suppress the CA1416 platform‑compatibility warning for ImageFormat.Bmp
#pragma warning disable CA1416 // ImageFormat.Bmp is Windows‑only but acceptable for this scenario
                while (converter.HasNextImage())
                {
                    // Build output file name, e.g., "BmpPages/page5.bmp"
                    string outputPath = Path.Combine(outputDir, $"page{pageIndex}.bmp");

                    // Save the current page as BMP
                    converter.GetNextImage(outputPath, ImageFormat.Bmp);

                    pageIndex++;
                }
#pragma warning restore CA1416
            }
        }

        Console.WriteLine("Pages 5‑7 have been saved as BMP images.");
    }
}
