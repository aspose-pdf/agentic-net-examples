using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class MergeImagesToPdf
{
    static void Main(string[] args)
    {
        // Input: list of image file paths (modify as needed)
        List<string> imagePaths = new List<string>
        {
            "image1.jpg",
            "image2.png",
            "image3.bmp"
        };

        // Output PDF file
        string outputPdf = "merged_images.pdf";

        // Validate that all images exist
        foreach (string imgPath in imagePaths)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Error: Image file not found - {imgPath}");
                return;
            }
        }

        // Create an empty PDF document
        Document pdfDocument = new Document();

        // Facade for adding images to the PDF
        PdfFileMend mend = new PdfFileMend(pdfDocument);

        // Add each image on a separate page
        for (int i = 0; i < imagePaths.Count; i++)
        {
            // Add a new blank page
            pdfDocument.Pages.Add();

            // Page numbers are 1‑based
            int pageNumber = i + 1;

            // Get page dimensions
            Page page = pdfDocument.Pages[pageNumber];
            float pageWidth = (float)page.PageInfo.Width;
            float pageHeight = (float)page.PageInfo.Height;

            // Add the image covering the whole page
            // Parameters: image path, page number, lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            mend.AddImage(imagePaths[i], pageNumber, 0, 0, pageWidth, pageHeight);
        }

        // Save the resulting PDF
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Images merged into PDF successfully: {outputPdf}");
    }
}