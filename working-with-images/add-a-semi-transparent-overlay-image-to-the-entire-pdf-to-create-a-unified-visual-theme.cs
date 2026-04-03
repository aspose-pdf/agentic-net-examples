using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string overlayImagePath = "overlay.png";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(overlayImagePath))
        {
            Console.Error.WriteLine($"Overlay image not found: {overlayImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Apply the overlay image to each page
            foreach (Page page in doc.Pages)
            {
                // Create an image stamp from the overlay image file
                ImageStamp imgStamp = new ImageStamp(overlayImagePath);

                // Place the stamp on top of the page content
                imgStamp.Background = false;

                // Set semi‑transparent opacity (0.0 – 1.0)
                imgStamp.Opacity = 0.5f;

                // Center the stamp on the page
                imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
                imgStamp.VerticalAlignment = VerticalAlignment.Center;

                // Scale the stamp to cover the entire page
                imgStamp.Width = page.PageInfo.Width;
                imgStamp.Height = page.PageInfo.Height;

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPath}'.");
    }
}