using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string imagePath = "banner.bmp";
        const string outputPath = "banner120x180.pdf";

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a new page
            Page page = doc.Pages.Add();

            // Set fixed page dimensions (points)
            page.PageInfo.Width = 120;
            page.PageInfo.Height = 180;
            page.PageInfo.Margin = new MarginInfo(0, 0, 0, 0);

            // Load the bitmap and place it on the page
            Image img = new Image
            {
                File = imagePath,
                FixWidth = page.PageInfo.Width,
                FixHeight = page.PageInfo.Height,
                IsInLineParagraph = false,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom,
                Margin = new MarginInfo(0, 0, 0, 0)
            };

            page.Paragraphs.Add(img);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to {outputPath}");
    }
}
