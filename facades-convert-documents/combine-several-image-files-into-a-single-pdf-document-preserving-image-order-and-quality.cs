using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ImageToPdfCombiner
{
    static void Main()
    {
        // Paths to the image files – order in the array defines the page order in the PDF
        string[] imageFiles = new[]
        {
            "image1.jpg",
            "image2.png",
            "image3.tif"
            // add more image paths as needed
        };

        const string outputPdfPath = "combined.pdf";

        // Create an empty PDF document that will receive the images
        using (Document pdfDoc = new Document())
        {
            // Facade that allows adding images to the PDF
            PdfFileMend pdfMend = new PdfFileMend();
            pdfMend.BindPdf(pdfDoc); // bind the empty document to the facade

            int pageNumber = 1; // Aspose.Pdf uses 1‑based page indexing

            foreach (string imgPath in imageFiles)
            {
                if (!File.Exists(imgPath))
                {
                    Console.Error.WriteLine($"Image not found: {imgPath}");
                    continue; // skip missing files but keep processing the rest
                }

                // Add a new blank page for the current image
                pdfDoc.Pages.Add();

                // Use the default page size (A4 = 595 x 842 points) to place the image.
                // The image will be scaled to fill the whole page while preserving its quality.
                const float lowerLeftX = 0f;
                const float lowerLeftY = 0f;
                const float upperRightX = 595f; // page width in points
                const float upperRightY = 842f; // page height in points

                // Add the image to the newly created page.
                // The overload that takes the image file name preserves the original image data.
                pdfMend.AddImage(imgPath, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

                pageNumber++;
            }

            // Save the assembled PDF. PdfFileMend inherits SaveableFacade, so Save(string) is valid.
            pdfMend.Save(outputPdfPath);
        }

        Console.WriteLine($"Images have been combined into PDF: {outputPdfPath}");
    }
}