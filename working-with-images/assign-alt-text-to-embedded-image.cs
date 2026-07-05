using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for the output PDF and the image to embed
        const string outputPdfPath = "output.pdf";
        const string imageFilePath = "image.png";

        // Alternative text that will be associated with the image
        const string altText = "A descriptive caption for the embedded image.";

        // Verify that the image file exists
        if (!File.Exists(imageFilePath))
        {
            Console.Error.WriteLine($"Image file not found: {imageFilePath}");
            return;
        }

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Create an Image object, set its source file, and add it to the page
            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
            {
                File = imageFilePath
            };
            page.Paragraphs.Add(pdfImage);

            // After the image is added, the XImage resource is available in the page's Resources.Images collection.
            // Iterate over the collection and assign the alternative text.
            foreach (Aspose.Pdf.XImage xImg in page.Resources.Images)
            {
                // TrySetAlternativeText returns true if the alt text was successfully set.
                bool success = xImg.TrySetAlternativeText(altText, page);
                if (!success)
                {
                    Console.Error.WriteLine("Failed to set alternative text for an image on the page.");
                }
            }

            // Save the PDF with the embedded image and its alt text
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved to '{outputPdfPath}' with image alt text assigned.");
    }
}