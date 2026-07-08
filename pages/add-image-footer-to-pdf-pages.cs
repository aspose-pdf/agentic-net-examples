using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string footerImagePath = "footer.png";
        const string outputPath = "output.pdf";

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

        // Load the source PDF (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page dimensions
                double pageWidth = page.Rect.Width;
                double pageHeight = page.Rect.Height;

                // Desired footer height (points). Adjust as needed.
                double footerHeight = 50;

                // Define rectangle at the bottom of the page.
                // Left = 0, Bottom = 0, Right = page width, Top = footerHeight.
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, pageWidth, footerHeight);

                // Add the image; it will be scaled proportionally to fit the rectangle.
                page.AddImage(footerImagePath, rect);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with image footer saved to '{outputPath}'.");
    }
}