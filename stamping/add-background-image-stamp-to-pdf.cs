using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string stampImg  = "stamp.png";
        const string outputPdf = "output.pdf";

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Get the first page to obtain its dimensions
            Page page = doc.Pages[1];

            // Create an ImageStamp from the image file
            ImageStamp stamp = new ImageStamp(stampImg)
            {
                // Appear behind existing page content
                Background = true,
                // Center the stamp on the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                // Resize the stamp to cover the whole page (optional)
                Width  = page.PageInfo.Width,
                Height = page.PageInfo.Height
            };

            // Apply the stamp to the first page (or iterate over all pages as needed)
            page.AddStamp(stamp);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
