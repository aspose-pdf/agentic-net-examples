using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // original PDF containing the JPEG on page 3
        const string pngImage  = "high_res.png"; // higher‑resolution PNG to replace the JPEG
        const string outputPdf = "output.pdf";   // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(pngImage))
        {
            Console.Error.WriteLine($"PNG image not found: {pngImage}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Ensure the document has at least three pages
            if (doc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The document does not contain a third page.");
                return;
            }

            // Access page three
            Page page = doc.Pages[3];

            // Get the image collection for the page
            XImageCollection images = page.Resources.Images;

            // Verify that the page contains at least one image to replace
            if (images.Count == 0)
            {
                Console.Error.WriteLine("No images found on page 3 to replace.");
                return;
            }

            // Replace the first image in the collection with the higher‑resolution PNG.
            // XImageCollection uses 1‑based indexing.
            using (FileStream pngStream = File.OpenRead(pngImage))
            {
                images.Replace(1, pngStream);
            }

            // Save the modified PDF (lifecycle rule: save inside the using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image replaced and saved to '{outputPdf}'.");
    }
}