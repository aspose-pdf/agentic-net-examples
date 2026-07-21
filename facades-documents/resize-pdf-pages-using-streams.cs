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

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Desired page dimensions (points; 1 point = 1/72 inch)
        const double targetWidth  = 1024; // points
        const double targetHeight = 768;  // points

        // Prepare streams for source and destination PDFs
        using (FileStream srcStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream destStream = new MemoryStream())
        {
            // Create the facade that performs the resize operation
            PdfFileEditor fileEditor = new PdfFileEditor();

            // Build resize parameters – new page size
            PdfFileEditor.ContentsResizeParameters resizeParams =
                PdfFileEditor.ContentsResizeParameters.PageResize(targetWidth, targetHeight);

            // Resize all pages (pages == null) using the stream overload
            bool success = fileEditor.ResizeContents(srcStream, destStream, null, resizeParams);
            if (!success)
            {
                Console.Error.WriteLine("Resize operation failed.");
                return;
            }

            // Reset stream position for subsequent reading
            destStream.Position = 0;

            // Verify visual fidelity by checking the resulting page size
            using (Document resizedDoc = new Document(destStream))
            {
                // Aspose.Pdf uses 1‑based page indexing
                Page firstPage = resizedDoc.Pages[1];
                double actualWidth  = firstPage.PageInfo.Width;
                double actualHeight = firstPage.PageInfo.Height;

                // Simple tolerance check (points)
                const double tolerance = 0.1;
                bool widthMatch  = Math.Abs(actualWidth  - targetWidth)  < tolerance;
                bool heightMatch = Math.Abs(actualHeight - targetHeight) < tolerance;

                Console.WriteLine($"Resize successful: {success}");
                Console.WriteLine($"First page size – Width: {actualWidth}, Height: {actualHeight}");
                Console.WriteLine($"Width match: {widthMatch}, Height match: {heightMatch}");
            }

            // Persist the resized PDF to disk
            using (FileStream outFs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                destStream.WriteTo(outFs);
            }
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}