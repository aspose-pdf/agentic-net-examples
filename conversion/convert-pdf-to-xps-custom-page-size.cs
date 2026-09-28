using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.xps";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Custom page size: 8.5 x 11 inches (points = inches * 72)
            // For landscape orientation, swap width and height (width > height)
            const double widthPoints = 11 * 72; // 792 points (landscape width)
            const double heightPoints = 8.5 * 72; // 612 points (landscape height)

            // Apply the custom size for each page
            for (int i = 1; i <= pdfDoc.Pages.Count; i++) // 1‑based indexing
            {
                Page page = pdfDoc.Pages[i];
                page.PageInfo.Width = widthPoints;
                page.PageInfo.Height = heightPoints;
                // Orientation is implied by width > height; no Orientation property exists.
            }

            // Save the document as XPS; must pass XpsSaveOptions explicitly
            XpsSaveOptions xpsOptions = new XpsSaveOptions();
            pdfDoc.Save(outputPath, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS with custom page size: {outputPath}");
    }
}
