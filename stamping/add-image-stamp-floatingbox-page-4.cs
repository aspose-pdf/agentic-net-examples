using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath  = "stamp.png";

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image stamp not found: {imagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document contains fewer than 4 pages.");
                return;
            }

            // Retrieve page 4
            Page page = doc.Pages[4];

            // Create a FloatingBox that will cover the whole page
            FloatingBox floatingBox = new FloatingBox
            {
                Width               = page.PageInfo.Width,
                Height              = page.PageInfo.Height,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Bottom
            };

            // Add the image as the first element inside the FloatingBox – it will act as a background
            Image backgroundImage = new Image
            {
                File      = imagePath,
                // Use FixWidth/FixHeight to set the size (Width/Height properties are not available in newer versions)
                FixWidth  = page.PageInfo.Width,
                FixHeight = page.PageInfo.Height,
                // Align the image to the bottom‑left of the FloatingBox (covers the whole area)
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Bottom
            };
            floatingBox.Paragraphs.Add(backgroundImage);

            // Add the FloatingBox to the page's paragraph collection
            page.Paragraphs.Add(floatingBox);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp applied as background inside a FloatingBox on page 4. Saved to '{outputPath}'.");
    }
}
