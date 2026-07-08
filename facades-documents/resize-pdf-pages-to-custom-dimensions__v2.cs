using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Open the source PDF as a stream and prepare a destination stream.
        using (FileStream srcStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream destStream = new MemoryStream())
        {
            // Facade that provides page‑content resizing.
            PdfFileEditor fileEditor = new PdfFileEditor();

            // Desired dimensions in points (1 point = 1/72 inch). At 72 DPI, points equal pixels.
            double targetWidth  = 1024; // 1024 px
            double targetHeight = 768;  // 768 px

            // Resize contents of all pages (pages = null) to the specified size.
            fileEditor.ResizeContents(srcStream, destStream, null, targetWidth, targetHeight);

            // Reset the destination stream position before reading from it.
            destStream.Position = 0;

            // Persist the resized PDF to a file.
            using (FileStream outFile = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
            {
                destStream.CopyTo(outFile);
            }
        }

        // Verify the result by reading the page dimensions of the output PDF.
        using (Document doc = new Document(outputPath))
        {
            // Assuming uniform page size; inspect the first page.
            Page firstPage = doc.Pages[1];
            double width  = firstPage.PageInfo.Width;  // points
            double height = firstPage.PageInfo.Height; // points

            Console.WriteLine($"Resized PDF page size: {width} x {height} points");
        }

        Console.WriteLine($"Resizing completed. Output saved to '{outputPath}'.");
    }
}