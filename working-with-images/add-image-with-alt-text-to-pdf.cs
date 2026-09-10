using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "output_with_image.pdf"; // result PDF
        const string imagePath = "picture.jpg";        // image to add
        const string altText   = "A scenic mountain view"; // descriptive alt text

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the image will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle where the image will appear (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Add the image to the page and to the page resources
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // This adds the image to the page resources and draws it at the specified rectangle
                page.AddImage(imgStream, rect);
            }

            // Retrieve the XImage that was just added.
            // XImageCollection is 1‑based; the newly added image is the last entry.
            XImage addedImage = page.Resources.Images[page.Resources.Images.Count];

            // Set alternative text for accessibility
            bool success = addedImage.TrySetAlternativeText(altText, page);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set alternative text for the image.");
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image added with alt text and saved to '{outputPdf}'.");
    }
}