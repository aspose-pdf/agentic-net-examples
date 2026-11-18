using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string bannerImg = "banner.jpg";
        const string outputPdf = "output.pdf";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(bannerImg))
        {
            Console.Error.WriteLine($"Banner image not found: {bannerImg}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least one page (the index page)
            if (doc.Pages.Count < 1)
            {
                Console.Error.WriteLine("The PDF contains no pages.");
                return;
            }

            // Access the first page (index page). Aspose.Pdf uses 1‑based indexing.
            Page indexPage = doc.Pages[1];

            // Remove all existing content from the page (paragraphs, images, etc.)
            indexPage.Paragraphs.Clear();

            // Create an ImageStamp for the banner. This class allows absolute positioning
            // without using the deprecated Image.Rectangle property.
            ImageStamp bannerStamp = new ImageStamp(bannerImg)
            {
                // Set the desired size – full page width and a fixed height (e.g., 100 points)
                Width = indexPage.PageInfo.Width,
                Height = 100,
                // Position the stamp at the top of the page (Y origin is bottom‑left)
                XIndent = 0,
                YIndent = indexPage.PageInfo.Height - 100
            };

            // Add the stamp to the page
            indexPage.AddStamp(bannerStamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Branding updated successfully: {outputPdf}");
    }
}
