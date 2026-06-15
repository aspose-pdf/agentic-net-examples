using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized.pdf";

        // Ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Resize PDF pages to 1024x768 points (default unit = points)
        //    using the stream overload of PdfFileEditor.ResizeContents.
        // -----------------------------------------------------------------
        using (FileStream srcStream  = new FileStream(inputPath,  FileMode.Open,  FileAccess.Read))
        using (FileStream dstStream  = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            PdfFileEditor fileEditor = new PdfFileEditor();

            // pages = null => all pages are processed
            // newWidth = 1024, newHeight = 768 (points)
            bool success = fileEditor.ResizeContents(srcStream, dstStream, null, 1024, 768);

            if (!success)
            {
                Console.Error.WriteLine("Resize operation failed.");
                return;
            }
        }

        // -----------------------------------------------------------------
        // 2. Verify visual fidelity by rendering the first page of the
        //    original and the resized PDF to images and comparing their
        //    pixel dimensions.
        // -----------------------------------------------------------------
        try
        {
            // Helper to render first page to a bitmap and return its size
            Size GetFirstPageSize(string pdfPath)
            {
                using (FileStream pdfStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
                {
                    PdfConverter converter = new PdfConverter();
                    converter.BindPdf(pdfStream);
                    converter.DoConvert();

                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        // Render first page as PNG (default resolution = 150 DPI)
                        converter.GetNextImage(imgStream, ImageFormat.Png);
                        using (Image img = Image.FromStream(imgStream))
                        {
                            return img.Size;
                        }
                    }
                }
            }

            Size originalSize = GetFirstPageSize(inputPath);
            Size resizedSize  = GetFirstPageSize(outputPath);

            Console.WriteLine($"Original first page image size : {originalSize.Width}x{originalSize.Height} pixels");
            Console.WriteLine($"Resized  first page image size : {resizedSize.Width}x{resizedSize.Height} pixels");

            // Simple visual fidelity check: ensure the resized image dimensions
            // match the requested 1024x768 (within a tolerance due to DPI scaling).
            const int targetWidth  = 1024;
            const int targetHeight = 768;
            const int tolerance    = 5; // pixels

            bool widthMatch  = Math.Abs(resizedSize.Width  - targetWidth)  <= tolerance;
            bool heightMatch = Math.Abs(resizedSize.Height - targetHeight) <= tolerance;

            if (widthMatch && heightMatch)
                Console.WriteLine("Resize verification passed: dimensions are as expected.");
            else
                Console.WriteLine("Resize verification warning: dimensions differ from expected values.");
        }
        catch (TypeInitializationException)
        {
            // GDI+ (System.Drawing) is unavailable on non‑Windows platforms.
            Console.WriteLine("Visual verification skipped: System.Drawing is not supported on this platform.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Verification error: {ex.Message}");
        }
    }
}