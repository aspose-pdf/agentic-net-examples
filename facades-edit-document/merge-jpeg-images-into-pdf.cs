using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Facades;      // Facade for adding images

class Program
{
    static void Main()
    {
        // List of JPEG files to be merged into the PDF.
        // Adjust the paths as needed.
        string[] imageFiles = new string[]
        {
            "image1.jpg",
            "image2.jpg",
            "image3.jpg"
        };

        const string outputPdf = "merged_images.pdf";

        // Create an empty PDF document.
        // The using block ensures the document is disposed correctly.
        using (Document pdfDoc = new Document())
        {
            // PdfFileMend provides methods to add images to pages.
            PdfFileMend pdfMend = new PdfFileMend(pdfDoc);

            int pageNumber = 0;

            foreach (string imgPath in imageFiles)
            {
                if (!File.Exists(imgPath))
                {
                    Console.Error.WriteLine($"Image not found: {imgPath}");
                    continue; // Skip missing files.
                }

                // Add a new blank page for the current image.
                pdfDoc.Pages.Add();
                pageNumber++;

                // Retrieve the newly added page to obtain its dimensions.
                Page page = pdfDoc.Pages[pageNumber];

                // Define the rectangle that covers the whole page.
                // Coordinates are in points (1 point = 1/72 inch).
                float lowerLeftX = 0f;
                float lowerLeftY = 0f;
                float upperRightX = (float)page.PageInfo.Width;   // Cast double to float
                float upperRightY = (float)page.PageInfo.Height;  // Cast double to float

                // Add the image to the current page, scaling it to fill the page.
                pdfMend.AddImage(imgPath, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            }

            // Save the assembled PDF.
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}
