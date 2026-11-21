using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        const int pageNumber = 1; // 1‑based indexing as required by Aspose.Pdf
        double newWidth;
        double newHeight;

        // 1️⃣ Read original page size.
        using (Document srcDoc = new Document(inputPath))
        {
            var originalRect = srcDoc.Pages[pageNumber].MediaBox;
            // Example: double the width and height to keep layout after rotation.
            newWidth = originalRect.Width * 2;
            newHeight = originalRect.Height * 2;
        }

        // 2️⃣ Resize the page and rotate it using the Document API (PdfPageEditor no longer provides ResizePage/RotatePage).
        using (Document pdf = new Document(inputPath))
        {
            var page = pdf.Pages[pageNumber];
            // Resize the page canvas.
            page.SetPageSize(newWidth, newHeight);
            // Rotate the page 90° clockwise.
            page.Rotate = Rotation.on90;
            pdf.Save(outputPath);
        }

        Console.WriteLine($"Page {pageNumber} resized to {newWidth}x{newHeight} and rotated 90°. Saved as '{outputPath}'.");
    }
}
