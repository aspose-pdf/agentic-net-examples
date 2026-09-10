using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // not strictly needed but safe for any text handling

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "uniform_pages.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: wrap Document in using)
        using (Document doc = new Document(inputPath))
        {
            // Determine the maximum width and height among all pages
            double maxWidth  = 0;
            double maxHeight = 0;

            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // Page.Rect is an Aspose.Pdf.Rectangle
                double width  = page.Rect.Width;
                double height = page.Rect.Height;

                if (width  > maxWidth)  maxWidth  = width;
                if (height > maxHeight) maxHeight = height;
            }

            // Resize each page to the largest dimensions
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];
                // PageSize constructor expects float values
                page.Resize(new PageSize((float)maxWidth, (float)maxHeight));
            }

            // Save the modified document (using rule: Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages resized and saved to '{outputPath}'.");
    }
}