using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input image files (modify the paths as needed)
        string[] imageFiles = new string[]
        {
            "image1.jpg",
            "image2.png",
            "image3.tif"
        };

        // Output PDF file
        const string outputPdf = "merged_images.pdf";

        // Verify that all image files exist before proceeding
        foreach (string imgPath in imageFiles)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image file not found: {imgPath}");
                return;
            }
        }

        // Create a new PDF document and ensure proper disposal
        using (Document pdfDoc = new Document())
        {
            // Add each image as a separate page
            foreach (string imgPath in imageFiles)
            {
                // Add a new blank page
                Page page = pdfDoc.Pages.Add();

                // Create an Image object and set its source file
                Image img = new Image
                {
                    File = imgPath
                };

                // Add the image to the page's content
                page.Paragraphs.Add(img);
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Images merged into PDF: {outputPdf}");
    }
}