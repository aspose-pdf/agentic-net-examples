using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // ImageStamp resides here

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";
        const string imagePath  = "high_res_image.jpg"; // path to the high‑resolution image

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a new page (page index is 1‑based)
            Page page = doc.Pages.Add();

            // Use ImageStamp for absolute positioning on the page
            ImageStamp stamp = new ImageStamp(imagePath)
            {
                // Absolute coordinates (points, 1 point = 1/72 inch)
                XIndent = 100, // distance from the left edge
                YIndent = 500, // distance from the bottom edge

                // Desired display size (optional – keep original size if omitted)
                Width  = 400,
                Height = 600
            };

            // Add the stamp to the page
            page.AddStamp(stamp);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with high‑resolution image saved to '{outputPath}'.");
    }
}
