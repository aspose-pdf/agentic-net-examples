using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        // Desired dimensions in pixels
        const double targetPixelWidth  = 1024;
        const double targetPixelHeight = 768;

        // Convert pixels to points (default PDF unit). Assuming 96 DPI.
        const double dpi = 96.0;
        double targetWidthPoints  = targetPixelWidth  * 72.0 / dpi; // 768 points
        double targetHeightPoints = targetPixelHeight * 72.0 / dpi; // 576 points

        // Ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Stream overload of PdfFileEditor.ResizeContents
        using (FileStream srcStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream destStream = new MemoryStream())
        {
            PdfFileEditor fileEditor = new PdfFileEditor();

            // Resize contents of all pages to the target size (in points)
            // Passing null for pages processes the entire document.
            bool success = fileEditor.ResizeContents(
                srcStream,
                destStream,
                null,               // all pages
                targetWidthPoints,  // new width in points
                targetHeightPoints  // new height in points
            );

            if (!success)
            {
                Console.Error.WriteLine("Resize operation failed.");
                return;
            }

            // Reset stream position for subsequent reading
            destStream.Position = 0;

            // Save the resized PDF to a physical file (optional, for inspection)
            using (FileStream outFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                destStream.CopyTo(outFile);
            }

            // Verify visual fidelity by checking page dimensions
            using (Document resizedDoc = new Document(destStream))
            {
                // Aspose.Pdf uses 1‑based page indexing
                Page firstPage = resizedDoc.Pages[1];
                double actualWidth  = firstPage.PageInfo.Width;
                double actualHeight = firstPage.PageInfo.Height;

                const double tolerance = 0.5; // points tolerance

                bool widthMatches  = Math.Abs(actualWidth  - targetWidthPoints)  <= tolerance;
                bool heightMatches = Math.Abs(actualHeight - targetHeightPoints) <= tolerance;

                Console.WriteLine($"Resize successful: {success}");
                Console.WriteLine($"Expected size (points): {targetWidthPoints} x {targetHeightPoints}");
                Console.WriteLine($"Actual size   (points): {actualWidth} x {actualHeight}");
                Console.WriteLine($"Width match:  {widthMatches}");
                Console.WriteLine($"Height match: {heightMatches}");
            }
        }
    }
}