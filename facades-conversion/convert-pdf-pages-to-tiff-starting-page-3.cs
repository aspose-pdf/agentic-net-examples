using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF file path
        const string inputPdf = "input.pdf";
        // Output folder for TIFF images
        const string outputDir = "TiffPages";

        // ---------------------------------------------------------------------
        // Ensure a PDF exists – the sandbox does not contain any files by default.
        // If the file is missing we create a minimal multi‑page PDF so that the
        // conversion (starting at page 3) has something to work with.
        // ---------------------------------------------------------------------
        if (!File.Exists(inputPdf))
        {
            var placeholder = new Document();
            // Create at least three pages because we start conversion from page 3.
            placeholder.Pages.Add(); // page 1
            placeholder.Pages.Add(); // page 2
            placeholder.Pages.Add(); // page 3 (and any additional pages you like)
            placeholder.Save(inputPdf);
        }

        // Make sure the output directory exists.
        Directory.CreateDirectory(outputDir);

        // Use PdfConverter inside a using block for deterministic disposal.
        using (PdfConverter converter = new PdfConverter())
        {
            // Bind the source PDF. The BindPdf overload that accepts a file path works
            // without the need to create a Document instance first.
            converter.BindPdf(inputPdf);

            // Determine the range of pages to convert.
            int startPage = 3;
            int endPage = converter.PageCount; // total pages in the PDF

            // Guard against a start page that exceeds the document length.
            if (startPage > endPage)
            {
                Console.WriteLine($"The document has only {endPage} page(s). No pages will be converted.");
                return;
            }

            // Convert each page individually to a TIFF image using the default resolution.
            for (int pageNumber = startPage; pageNumber <= endPage; pageNumber++)
            {
                // Configure the converter to process a single page.
                converter.StartPage = pageNumber;
                converter.EndPage   = pageNumber;

                // Prepare the conversion (required before any SaveAs* call).
                converter.DoConvert();

                // Build the output file name, e.g., "page_3.tiff".
                string outputPath = Path.Combine(outputDir, $"page_{pageNumber}.tiff");

                // Save the current page as a TIFF image. Default resolution is used automatically.
                converter.SaveAsTIFF(outputPath);
            }
        }

        Console.WriteLine($"Conversion completed. TIFF images are saved in the '{outputDir}' folder.");
    }
}
