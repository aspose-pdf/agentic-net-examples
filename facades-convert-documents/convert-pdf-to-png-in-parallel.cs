using System;
using System.IO;
using System.Threading.Tasks;
using System.Drawing.Imaging;          // ImageFormat enum
using Aspose.Pdf.Facades;             // PdfConverter, PdfFileEditor

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputDir = "output_images";

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        // Split the PDF into individual page streams (one stream per page)
        PdfFileEditor editor = new PdfFileEditor();
        MemoryStream[] pageStreams = editor.SplitToPages(inputPdf);

        // Convert each page to PNG in parallel
        Parallel.For(0, pageStreams.Length, i =>
        {
            // Each stream must be disposed after use
            using (MemoryStream pageStream = pageStreams[i])
            {
                // Create a new PdfConverter for this thread
                using (PdfConverter converter = new PdfConverter())
                {
                    // Bind the single‑page stream to the converter
                    converter.BindPdf(pageStream);
                    // Prepare the converter
                    converter.DoConvert();

                    // Build output file name (pages are 1‑based)
                    string outputPath = Path.Combine(outputDir, $"page_{i + 1}.png");

                    // Save the page as PNG
                    converter.GetNextImage(outputPath, ImageFormat.Png);
                }
            }
        });

        Console.WriteLine("PDF to PNG conversion completed.");
    }
}