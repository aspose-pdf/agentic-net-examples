using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string outputPdf = "output.pdf";  // result PDF
        const string newPng    = "high_res.png"; // replacement image

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(newPng))
        {
            Console.Error.WriteLine($"Replacement image not found: {newPng}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document has fewer than 3 pages.");
                return;
            }

            // Access page three
            Page page = doc.Pages[3];

            // Access the image collection of the page
            XImageCollection images = page.Resources.Images;

            // If there is at least one image, replace the first one
            if (images.Count > 0)
            {
                // Replace image at index 1 (1‑based) with the new PNG stream
                using (FileStream pngStream = File.OpenRead(newPng))
                {
                    images.Replace(1, pngStream);
                }
            }
            else
            {
                Console.WriteLine("No images found on page 3 to replace.");
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image replaced and saved to '{outputPdf}'.");
    }
}