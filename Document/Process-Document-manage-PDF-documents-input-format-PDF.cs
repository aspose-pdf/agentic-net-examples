using System;
using System.IO;
using System.Drawing.Imaging;               // Required for ImageFormat (Windows‑only GDI+)
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class PdfProcessor
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string imgOutDir  = "ExtractedImages";
        const string pageImgDir = "PageImages";
        const string tiffPath   = "AllPages.tiff";

        // Validate input
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Extract embedded images from the PDF (PdfExtractor)
        // -----------------------------------------------------------------
        Directory.CreateDirectory(imgOutDir);
        try
        {
            using (PdfExtractor extractor = new PdfExtractor())
            {
                // Bind the source PDF
                extractor.BindPdf(inputPdf);

                // Configure extraction mode (optional – default extracts all)
                // extractor.ExtractImageMode = ExtractImageMode.DefinedInResources;

                // Start image extraction
                extractor.ExtractImage();

                int imgIndex = 1;
                while (extractor.HasNextImage())
                {
                    string outFile = Path.Combine(imgOutDir, $"image_{imgIndex}.png");
                    // GetNextImage writes the image using the supplied ImageFormat.
                    // PNG is chosen to preserve transparency when present.
                    extractor.GetNextImage(outFile, ImageFormat.Png);
                    Console.WriteLine($"Extracted image: {outFile}");
                    imgIndex++;
                }
            }
        }
        catch (TypeInitializationException tie)
        {
            // GDI+ not available (e.g., non‑Windows platforms)
            Console.WriteLine("Image extraction requires GDI+. Skipping this step.");
            Console.WriteLine($"Details: {tie.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during image extraction: {ex.Message}");
        }

        // -----------------------------------------------------------------
        // 2. Convert each PDF page to a separate JPEG image (PdfConverter)
        // -----------------------------------------------------------------
        Directory.CreateDirectory(pageImgDir);
        try
        {
            using (PdfConverter converter = new PdfConverter())
            {
                // Bind the source PDF
                converter.BindPdf(inputPdf);

                // Prepare the converter (initialises internal structures)
                converter.DoConvert();

                int pageIndex = 1;
                while (converter.HasNextImage())
                {
                    string outFile = Path.Combine(pageImgDir, $"page_{pageIndex}.jpg");
                    // GetNextImage writes the image using the default JPEG format.
                    converter.GetNextImage(outFile);
                    Console.WriteLine($"Converted page {pageIndex} to image: {outFile}");
                    pageIndex++;
                }
            }
        }
        catch (TypeInitializationException tie)
        {
            // GDI+ not available – page‑to‑image conversion cannot run.
            Console.WriteLine("Page conversion to images requires GDI+. Skipping this step.");
            Console.WriteLine($"Details: {tie.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during page conversion: {ex.Message}");
        }

        // -----------------------------------------------------------------
        // 3. Convert the whole PDF to a multi‑page TIFF (PdfConverter)
        // -----------------------------------------------------------------
        try
        {
            using (PdfConverter tiffConverter = new PdfConverter())
            {
                tiffConverter.BindPdf(inputPdf);
                tiffConverter.DoConvert();

                // Save all pages as a single TIFF file.
                // Using default settings (150 DPI, no compression).
                tiffConverter.SaveAsTIFF(tiffPath);
                Console.WriteLine($"All pages saved as TIFF: {tiffPath}");
            }
        }
        catch (TypeInitializationException tie)
        {
            // GDI+ not available – TIFF conversion cannot run.
            Console.WriteLine("TIFF conversion requires GDI+. Skipping this step.");
            Console.WriteLine($"Details: {tie.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during TIFF conversion: {ex.Message}");
        }

        Console.WriteLine("PDF processing completed.");
    }
}