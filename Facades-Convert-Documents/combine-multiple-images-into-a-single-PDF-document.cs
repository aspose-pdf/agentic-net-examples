using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source images
        string[] imageFiles = { "image1.jpg", "image2.png", "image3.bmp" };
        // Destination PDF file
        const string outputPdf = "combined.pdf";

        // Verify that all image files exist before proceeding
        foreach (var imgPath in imageFiles)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image not found: {imgPath}");
                return;
            }
        }

        // Create an empty PDF document
        using (Document doc = new Document())
        {
            // PdfFileMend is a Facade that allows adding images to pages
            using (PdfFileMend pdfMend = new PdfFileMend(doc))
            {
                int pageNumber = 1;
                foreach (var imgPath in imageFiles)
                {
                    // Add a new blank page for each image
                    doc.Pages.Add();

                    // Add the image to the newly created page.
                    // The four coordinate parameters (llx, lly, urx, ury) are set to 0,
                    // which lets Aspose.Pdf place the image using its default sizing.
                    pdfMend.AddImage(imgPath, pageNumber, 0, 0, 0, 0);
                    pageNumber++;
                }
            }

            // Save the resulting PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Successfully merged images into '{outputPdf}'.");
    }
}