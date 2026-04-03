using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // not required but kept for completeness

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // existing PDF
        const string outputPdf = "output_with_image.pdf";
        const string imagePath = "newImage.png";       // image to add
        const string altText   = "A descriptive caption for the newly added image.";

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

        // Load the existing PDF, add an image, set its alternative text, and save.
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the image will be placed (first page in this example).
            Page page = doc.Pages[1];

            // Create an Image object, set its source file, and add it to the page's content.
            Image pdfImage = new Image();
            pdfImage.File = imagePath;
            page.Paragraphs.Add(pdfImage);

            // After adding the image, retrieve the corresponding XImage from the page resources.
            // The newly added image will be the last entry in the Images collection.
            XImage xImg = null;
            foreach (XImage img in page.Resources.Images)
            {
                xImg = img; // iterate to the last image
            }

            // Set alternative text for the XImage on the page.
            if (xImg != null)
            {
                bool success = xImg.TrySetAlternativeText(altText, page);
                if (!success)
                {
                    Console.Error.WriteLine("Failed to set alternative text for the image.");
                }
            }

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with image and alt text: {outputPdf}");
    }
}