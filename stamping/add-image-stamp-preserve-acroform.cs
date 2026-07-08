using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfPath  = "output.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the PDF document (AcroForm fields are preserved by default)
        using (Document doc = new Document(inputPdfPath))
        {
            // Apply the image stamp to each page (1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Create an ImageStamp from the image file
                ImageStamp imgStamp = new ImageStamp(stampImagePath)
                {
                    // Optional visual settings
                    Background = false,                     // stamp on top of page content
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    Opacity = 0.5f                         // semi‑transparent
                };

                // Add the stamp to the current page
                doc.Pages[pageNum].AddStamp(imgStamp);
            }

            // Save the modified PDF; AcroForm fields remain intact
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdfPath}'.");
    }
}