using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // result PDF
        const string stampImg  = "stamp.png";      // image to use as background

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImg))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImg}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least four pages
            if (doc.Pages.Count < 4)
            {
                Console.Error.WriteLine("The document does not contain a fourth page.");
                return;
            }

            // Get page four (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[4];

            // Create a FloatingBox that covers the whole page (constructor expects float values)
            FloatingBox floatingBox = new FloatingBox((float)page.PageInfo.Width, (float)page.PageInfo.Height);

            // Load the image and assign it as the background of the FloatingBox
            Image background = new Image();
            background.File = stampImg;
            floatingBox.BackgroundImage = background;

            // Add the FloatingBox to the page's paragraph collection
            page.Paragraphs.Add(floatingBox);

            // Save the modified document (lifecycle rule: save inside using)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with image background in FloatingBox on page 4: {outputPdf}");
    }
}
