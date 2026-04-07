using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
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

        // Load the PDF document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp from the image file
                ImageStamp imgStamp = new ImageStamp(imagePath);

                // Set the stamp width to the full page width (minus optional margins)
                // PageInfo provides the page dimensions in points.
                double pageWidth = page.PageInfo.Width;
                imgStamp.Width = pageWidth;

                // Preserve aspect ratio by letting the stamp calculate height automatically.
                // When only Width is set, the Height is scaled proportionally.
                // Align the stamp to the top of the page (you can change VerticalAlignment if needed)
                imgStamp.VerticalAlignment = VerticalAlignment.Top;
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (lifecycle rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPdf}'.");
    }
}