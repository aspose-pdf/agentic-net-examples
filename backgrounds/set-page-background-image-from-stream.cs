using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (contains Document, Page, Image)

class Program
{
    static void Main()
    {
        const string imagePath  = "background.jpg"; // path to the background image
        const string outputPath = "output.pdf";     // path for the generated PDF

        // Ensure the image file exists before proceeding
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to the document
            Page page = doc.Pages.Add();

            // Load the image from a file stream and set it as the page background
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                // Aspose.Pdf.Image represents an image resource that can be used as a background
                Image bgImage = new Image
                {
                    // Assign the stream directly; alternatively you could set bgImage.File = imagePath;
                    ImageStream = imgStream
                };

                // Set the page background image
                page.BackgroundImage = bgImage;
            }

            // Save the PDF to the specified output path
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF created with background image and saved to '{outputPath}'.");
    }
}
