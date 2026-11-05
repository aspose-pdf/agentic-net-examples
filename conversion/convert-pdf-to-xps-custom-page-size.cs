using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Devices; // XpsSaveOptions resides in Aspose.Pdf namespace, but Devices also contains related types

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputXpsPath = "output.xps";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Open the source PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Define custom page dimensions (points: 1 inch = 72 points)
            // Example: 8.5" x 11" => 612 x 792 points
            const double customWidth  = 612; // 8.5 inches
            const double customHeight = 792; // 11 inches

            // Apply the custom size and set landscape orientation to each page
            foreach (Page page in pdfDoc.Pages)
            {
                page.PageInfo.Width  = customWidth;
                page.PageInfo.Height = customHeight;
                page.PageInfo.IsLandscape = true; // true = landscape, false = portrait
            }

            // Prepare XPS save options (required for non‑PDF output)
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the document as XPS using the options
            pdfDoc.Save(outputXpsPath, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS: {outputXpsPath}");
    }
}