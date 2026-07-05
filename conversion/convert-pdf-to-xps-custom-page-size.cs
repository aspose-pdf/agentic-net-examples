using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputXps = "output.xps";

        // Define custom page size (width x height) in points.
        // Example: Landscape Letter size (11" x 8.5") -> 11*72 = 792, 8.5*72 = 612
        double customWidth  = 11 * 72; // 792 points
        double customHeight = 8.5 * 72; // 612 points

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block (deterministic disposal)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Apply the custom size and orientation to every page (1‑based indexing)
            for (int i = 1; i <= pdfDoc.Pages.Count; i++)
            {
                Page page = pdfDoc.Pages[i];
                // Use PageInfo to set dimensions and orientation
                page.PageInfo.Width = customWidth;
                page.PageInfo.Height = customHeight;
                page.PageInfo.IsLandscape = true; // optional, width > height already implies landscape
            }

            // Initialize XPS save options
            XpsSaveOptions xpsOptions = new XpsSaveOptions();

            // Save the document as XPS using the explicit save options
            pdfDoc.Save(outputXps, xpsOptions);
        }

        Console.WriteLine($"PDF successfully converted to XPS with custom page size: {outputXps}");
    }
}
