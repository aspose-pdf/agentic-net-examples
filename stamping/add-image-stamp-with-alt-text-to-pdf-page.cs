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
        const string altText = "Company logo";

        // Verify required files exist
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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least three pages (Aspose.Pdf uses 1‑based indexing)
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("Document has fewer than 3 pages.");
                return;
            }

            // Retrieve page three
            Page page = doc.Pages[3];

            // Create an image stamp from the PNG file
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Example positioning – top‑right corner of the page
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Top,
                Opacity = 1.0,
                Background = false
            };

            // Add the stamp to page three
            page.AddStamp(imgStamp);

            // After stamping, the image becomes part of the page's resource collection.
            // Set alternative text for each image resource to improve accessibility.
            foreach (XImage img in page.Resources.Images)
            {
                // TrySetAlternativeText attaches alt text to the image resource.
                img.TrySetAlternativeText(altText, page);
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image stamp with alternative text added to page 3. Saved as '{outputPdf}'.");
    }
}