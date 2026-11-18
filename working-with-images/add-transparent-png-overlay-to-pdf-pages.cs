using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string overlayPng = "overlay.png";
        const string outputPdf  = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(overlayPng))
        {
            Console.Error.WriteLine($"Overlay PNG not found: {overlayPng}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp from the transparent PNG
                ImageStamp stamp = new ImageStamp(overlayPng)
                {
                    // Set Background = false so the stamp appears above existing content
                    Background = false,

                    // Optional: stretch the overlay to fill the page
                    // Width = page.PageInfo.Width,
                    // Height = page.PageInfo.Height,
                    // HorizontalAlignment = HorizontalAlignment.Center,
                    // VerticalAlignment = VerticalAlignment.Center
                };

                // Add the stamp to the current page (per‑page call)
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Overlay applied and saved to '{outputPdf}'.");
    }
}