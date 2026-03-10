using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "sample.pdf";

        // Verify the file exists before attempting to open it
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(pdfPath))
        {
            // Aspose.Pdf uses 1‑based indexing for pages
            Page page = doc.Pages[1];

            // Retrieve the page rectangle (MediaBox by default)
            Aspose.Pdf.Rectangle rect = page.Rect;

            // Calculate width and height in points (1 point = 1/72 inch)
            double width  = rect.URX - rect.LLX;
            double height = rect.URY - rect.LLY;

            Console.WriteLine($"Page 1 dimensions: width = {width} pt, height = {height} pt");
        }
    }
}