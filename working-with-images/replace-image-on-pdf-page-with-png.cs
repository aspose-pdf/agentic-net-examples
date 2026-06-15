using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the high‑resolution PNG and the output PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string newPngPath    = "highres.png";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(newPngPath))
        {
            Console.Error.WriteLine($"Replacement PNG not found: {newPngPath}");
            return;
        }

        // Load the PDF document
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the document has at least three pages
            if (pdfDoc.Pages.Count < 3)
            {
                Console.Error.WriteLine("The PDF does not contain a third page.");
                return;
            }

            // Get page three
            Page pageThree = pdfDoc.Pages[3];

            // Access the image resources of that page
            var imageCollection = pageThree.Resources.Images;

            // If there are no images on the page, nothing to replace
            if (imageCollection.Count == 0)
            {
                Console.Error.WriteLine("No images found on page 3 to replace.");
                return;
            }

            // Replace the first image in the collection (index is 1‑based)
            // The Replace method accepts a stream containing the new image data.
            // Although the documentation mentions JPEG, the method works with PNG as well.
            using (FileStream pngStream = File.OpenRead(newPngPath))
            {
                imageCollection.Replace(1, pngStream);
            }

            // Save the modified PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image on page 3 replaced successfully. Output saved to '{outputPdfPath}'.");
    }
}