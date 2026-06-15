using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string footerImagePath = "footer.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(footerImagePath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerImagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Add the footer image to each page
            foreach (Page page in doc.Pages)
            {
                // Desired footer height (points)
                double footerHeight = 50;

                // Page width (points)
                double pageWidth = page.Rect.Width;

                // Define rectangle at the bottom of the page
                Aspose.Pdf.Rectangle footerRect = new Aspose.Pdf.Rectangle(
                    0,               // left (llx)
                    0,               // bottom (lly)
                    pageWidth,       // right (urx)
                    footerHeight);   // top (ury)

                // Add the image; it will be scaled proportionally to fit the rectangle
                page.AddImage(footerImagePath, footerRect);
            }

            // Save the modified PDF (lifecycle rule: using ensures proper disposal)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with image footer saved to '{outputPath}'.");
    }
}