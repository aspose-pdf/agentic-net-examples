using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Apply rotation, size, and a simulated zoom to each page
        foreach (Page page in pdfDocument.Pages)
        {
            // 1. Rotate 90 degrees clockwise
            page.Rotate = Rotation.on90; // valid enum values: on90, on180, on270, None

            // 2. Set page size to A4 using width/height doubles (required overload)
            page.SetPageSize(PageSize.A4.Width, PageSize.A4.Height);

            // 3. Simulate a zoom factor of 150% by scaling the page dimensions
            //    (Zoom is a viewer‑side concept; to reflect it in the file we enlarge the page size)
            const double zoomFactor = 1.5;
            page.PageInfo.Width  = page.PageInfo.Width  * zoomFactor;
            page.PageInfo.Height = page.PageInfo.Height * zoomFactor;
        }

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
