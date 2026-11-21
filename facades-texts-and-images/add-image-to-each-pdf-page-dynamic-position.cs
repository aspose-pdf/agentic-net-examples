using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_image.pdf";
        const string imagePath = "logo.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Iterate over every page (1‑based indexing).
            foreach (Page page in doc.Pages)
            {
                double pageWidth  = page.PageInfo.Width;
                double pageHeight = page.PageInfo.Height;

                // Create an ImageStamp for the logo.
                ImageStamp stamp = new ImageStamp(imagePath)
                {
                    Width  = 100,   // desired width in points
                    Height = 100    // desired height in points
                };

                // Center the image on the current page.
                stamp.XIndent = (float)((pageWidth  - stamp.Width)  / 2);
                stamp.YIndent = (float)((pageHeight - stamp.Height) / 2);

                // Add the stamp to the page.
                page.AddStamp(stamp);
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image placed on each page and saved to '{outputPdf}'.");
    }
}
