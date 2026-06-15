using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the new PDF, the image to add, and the desired alternative text.
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "image.png";
        const string altText       = "A scenic mountain landscape with a lake in the foreground.";

        // Ensure the image file exists before proceeding.
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Create a new PDF document and add a page.
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Add the image to the page's resources and obtain its resource name.
            string imageName;
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                imageName = page.Resources.Images.Add(imgStream);
            }

            // Retrieve the XImage object from the resources collection.
            XImage xImg = page.Resources.Images[imageName];

            // Set alternative text for the XImage on the page.
            // Returns true if the alt text was successfully applied.
            bool altSet = xImg.TrySetAlternativeText(altText, page);
            if (!altSet)
            {
                Console.Error.WriteLine("Failed to set alternative text for the image.");
            }

            // Add a visual representation of the image to the page.
            // This uses the high‑level Image class which references the resource we just added.
            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
            pdfImage.File = imagePath;
            page.Paragraphs.Add(pdfImage);

            // Save the resulting PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with image and alt text saved to '{outputPdfPath}'.");
    }
}