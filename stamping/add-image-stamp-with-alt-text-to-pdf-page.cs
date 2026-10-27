using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string stampImagePath = "stamp.png";
        const string altText = "Company logo stamp";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        try
        {
            using (Document doc = new Document(inputPdf))
            {
                // Ensure the document has at least three pages (1‑based indexing)
                if (doc.Pages.Count < 3)
                {
                    Console.Error.WriteLine("Document has fewer than 3 pages.");
                    return;
                }

                // Create an image stamp
                ImageStamp stamp = new ImageStamp(stampImagePath)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    Background = false,   // place on top of existing content
                    Opacity = 0.8         // optional transparency
                };

                // Add the stamp to page 3
                doc.Pages[3].AddStamp(stamp);

                // Set alternative text for the image resource on page 3
                foreach (XImage img in doc.Pages[3].Resources.Images)
                {
                    // Links alt text to the image on the specified page
                    img.TrySetAlternativeText(altText, doc.Pages[3]);
                }

                // Save the modified PDF
                doc.Save(outputPdf);
            }

            Console.WriteLine($"Image stamp with alt text added to page 3. Saved as '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}