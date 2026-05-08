using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string footerImagePath = "footer.png";
        const double scaleFactor = 0.5; // Scale the image to 50% of page width

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(footerImagePath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Get page dimensions
                double pageWidth  = page.Rect.Width;
                double pageHeight = page.Rect.Height;

                // Compute desired image width (scaled) and a proportional height.
                // Since we don't have the original image dimensions without loading it,
                // we set the image width to the scaled page width and a fixed footer height.
                double footerHeight = 50; // points; adjust as needed

                // Define rectangle positioned at the bottom of the page
                // (origin is lower‑left corner)
                Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(
                    0,                     // llx
                    0,                     // lly (bottom of page)
                    pageWidth,             // urx (full page width)
                    footerHeight);         // ury

                // Add the image to the page within the rectangle.
                // The AddImage method scales the image to fit the rectangle while preserving aspect ratio.
                page.AddImage(footerImagePath, footerRect);
            }

            // Save the modified PDF (lifecycle rule: use Save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with image footer saved to '{outputPath}'.");
    }
}