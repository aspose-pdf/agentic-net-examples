using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based page indexing (see global rule)
            Page page = doc.Pages[1];

            // Example cropping: define a new rectangle (llx, lly, urx, ury)
            // Use fully qualified Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle cropRect = new Aspose.Pdf.Rectangle(50, 50, 500, 800);
            page.CropBox = cropRect;

            // After cropping, retrieve the TrimBox values.
            // If TrimBox was not explicitly set, Aspose.Pdf returns the MediaBox.
            Aspose.Pdf.Rectangle trimBox = page.TrimBox;

            Console.WriteLine($"Page 1 TrimBox:");
            Console.WriteLine($"  LLX = {trimBox.LLX}");
            Console.WriteLine($"  LLY = {trimBox.LLY}");
            Console.WriteLine($"  URX = {trimBox.URX}");
            Console.WriteLine($"  URY = {trimBox.URY}");
        }
    }
}