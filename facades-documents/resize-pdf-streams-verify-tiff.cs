using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Devices; // <-- added for Resolution

class PdfResizeExample
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string resizedPdfPath = "resized.pdf";
        const string previewImagePath = "page1_resized.tiff"; // TIFF used for cross‑platform conversion

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a read‑only stream
        using (FileStream srcStream = File.OpenRead(inputPath))
        // Destination stream will hold the resized PDF in memory
        using (MemoryStream destStream = new MemoryStream())
        {
            // PdfFileEditor provides the ResizeContents overload that works with streams
            PdfFileEditor fileEditor = new PdfFileEditor();

            // Resize all pages (pages == null) to 1024x768 points.
            // Aspose.Pdf uses points (1/72 inch) as default units; assuming 72 DPI,
            // 1 point ≈ 1 pixel, so we pass pixel dimensions directly.
            bool success = fileEditor.ResizeContents(
                srcStream,          // source PDF stream
                destStream,         // destination PDF stream
                null,               // null = all pages (1‑based indexing internally)
                1024,               // new width in points
                768);               // new height in points

            if (!success)
            {
                Console.Error.WriteLine("Resize operation failed.");
                return;
            }

            // Ensure the destination stream is ready for reading
            destStream.Position = 0;

            // Persist the resized PDF to disk (optional, demonstrates save lifecycle)
            using (FileStream fileOut = File.Create(resizedPdfPath))
            {
                destStream.CopyTo(fileOut);
            }

            // Reset position again because CopyTo leaves the stream at the end
            destStream.Position = 0;

            // Load the resized PDF to verify visual fidelity
            using (Document resizedDoc = new Document(destStream))
            {
                // Use PdfConverter to rasterize the first page.
                PdfConverter converter = new PdfConverter(resizedDoc);
                converter.StartPage = 1;
                converter.EndPage   = 1;
                // Set a reasonable resolution; 150 DPI yields a clear preview.
                converter.Resolution = new Resolution(150); // <-- now resolves correctly
                converter.DoConvert();

                // Export the first page as a TIFF (cross‑platform safe).
                converter.SaveAsTIFF(previewImagePath);

                // Clean up the converter
                converter.Close();
            }

            Console.WriteLine($"Resized PDF saved to '{resizedPdfPath}'.");
            Console.WriteLine($"Preview image saved to '{previewImagePath}'.");
        }
    }
}
