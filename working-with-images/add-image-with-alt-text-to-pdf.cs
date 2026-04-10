using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // Existing PDF
        const string outputPdfPath = "output.pdf";         // Result PDF
        const string imagePath     = "picture.jpg";        // Image to add
        const string altText       = "A scenic mountain view";

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the image will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Open the image file as a stream
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // 1) Add the image to the page resources.
                // The Add method returns the internal name of the image resource.
                string imageResourceName = page.Resources.Images.Add(imgStream);

                // Reset the stream position so it can be read again when creating the Image object.
                imgStream.Position = 0;

                // 2) Insert the image into the page content.
                Image pdfImage = new Image
                {
                    ImageStream = imgStream   // Use the same stream for the visual appearance
                };
                page.Paragraphs.Add(pdfImage);

                // 3) Set alternative text for the image resource.
                // Retrieve the XImage instance by its resource name.
                XImage xImg = page.Resources.Images[imageResourceName];
                bool altSet = xImg.TrySetAlternativeText(altText, page);

                if (!altSet)
                {
                    Console.Error.WriteLine("Failed to set alternative text for the image.");
                }
            }

            // Save the modified document.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF saved with image and alt text to '{outputPdfPath}'.");
    }
}