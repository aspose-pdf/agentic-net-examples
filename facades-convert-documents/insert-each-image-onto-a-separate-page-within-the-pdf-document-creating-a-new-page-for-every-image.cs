using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input image files – adjust the paths as needed
        string[] imageFiles = new string[]
        {
            "image1.jpg",
            "image2.png",
            "image3.bmp"
        };

        const string outputPdf = "combined_images.pdf";

        // Validate that all image files exist before proceeding
        foreach (string imgPath in imageFiles)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image not found: {imgPath}");
                return;
            }
        }

        // Create a new PDF document. The Document class implements IDisposable,
        // so we wrap it in a using block for deterministic disposal.
        using (Document pdfDoc = new Document())
        {
            // PdfFileMend is a Facade that allows adding images to existing pages.
            // It also implements IDisposable, so we use a using block.
            using (PdfFileMend mend = new PdfFileMend(pdfDoc))
            {
                // Iterate over each image, create a new page, and place the image on it.
                foreach (string imgPath in imageFiles)
                {
                    // Add a new blank page to the document.
                    // Page indexing in Aspose.Pdf is 1‑based.
                    pdfDoc.Pages.Add();

                    // Determine the page number of the newly added page.
                    int pageNumber = pdfDoc.Pages.Count;

                    // Open the image file as a stream.
                    using (FileStream imgStream = File.OpenRead(imgPath))
                    {
                        // Define the rectangle where the image will be placed.
                        // Here we use the full page size (0,0) to (500,800) as an example.
                        // Adjust the coordinates as needed for your specific layout.
                        float lowerLeftX = 0f;
                        float lowerLeftY = 0f;
                        float upperRightX = 500f;
                        float upperRightY = 800f;

                        // Add the image to the specified page.
                        // The AddImage method returns a bool indicating success.
                        bool added = mend.AddImage(imgStream, pageNumber,
                                                   lowerLeftX, lowerLeftY,
                                                   upperRightX, upperRightY);
                        if (!added)
                        {
                            Console.Error.WriteLine($"Failed to add image {imgPath} to page {pageNumber}");
                        }
                    }
                }

                // Save the resulting PDF. Document.Save(string) writes a PDF regardless of extension.
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}