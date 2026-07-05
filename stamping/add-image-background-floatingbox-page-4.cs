using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF
        const string imagePath = "stamp.png";   // image to use as background
        const string outputPath = "output.pdf"; // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least four pages
            if (doc.Pages.Count < 4)
            {
                while (doc.Pages.Count < 4)
                {
                    doc.Pages.Add();
                }
            }

            // Get page four (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[4];

            // Create a FloatingBox that covers the whole page – constructor expects float values
            FloatingBox floatingBox = new FloatingBox(
                (float)page.PageInfo.Width,
                (float)page.PageInfo.Height);

            // Load the image and assign it as the background of the FloatingBox
            floatingBox.BackgroundImage = new Image { File = imagePath };

            // Optionally set the ZIndex to a negative value so the box stays behind other content
            floatingBox.ZIndex = -1;

            // Add the FloatingBox to the page's paragraph collection
            page.Paragraphs.Add(floatingBox);

            // Save the modified PDF (lifecycle rule: save inside the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with image background in FloatingBox on page 4: {outputPath}");
    }
}
