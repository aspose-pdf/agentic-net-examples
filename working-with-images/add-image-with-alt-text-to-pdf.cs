using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "newImage.png";
        const string altText       = "Description of the newly added image for assistive technologies";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPdfPath))
        {
            // Add the image to the first page
            Page page = doc.Pages[1];
            Aspose.Pdf.Image img = new Aspose.Pdf.Image
            {
                File = imagePath,
                // Optional: set explicit size (width/height in points)
                FixWidth  = 200,
                FixHeight = 150
            };
            page.Paragraphs.Add(img);

            // Set alternative text for the newly added image
            foreach (XImage xImg in page.Resources.Images)
            {
                // TrySetAlternativeText returns true if the alt text was applied
                xImg.TrySetAlternativeText(altText, page);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with image and alt text: {outputPdfPath}");
    }
}