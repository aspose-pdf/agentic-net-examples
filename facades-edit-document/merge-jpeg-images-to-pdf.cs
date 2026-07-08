using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the JPEG images to be merged
        string[] imageFiles = new string[]
        {
            "image1.jpg",
            "image2.jpg",
            "image3.jpg"
        };

        // Output PDF file
        const string outputPdf = "merged.pdf";

        // Validate that all image files exist before proceeding
        foreach (string imgPath in imageFiles)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image file not found: {imgPath}");
                return;
            }
        }

        // Create a new PDF document and use PdfFileMend (Facade) to add images
        using (Document pdfDoc = new Document())
        {
            // PdfFileMend works on an existing Document instance
            PdfFileMend pdfMend = new PdfFileMend(pdfDoc);

            for (int i = 0; i < imageFiles.Length; i++)
            {
                // Add a blank page for the current image
                pdfDoc.Pages.Add();

                // Page numbers in Aspose.Pdf are 1‑based
                int pageNumber = i + 1;

                // Retrieve page dimensions (width and height) for full‑page placement
                // PageInfo.Width/Height are double, cast to float for AddImage overload
                float pageWidth  = (float)pdfDoc.Pages[pageNumber].PageInfo.Width;
                float pageHeight = (float)pdfDoc.Pages[pageNumber].PageInfo.Height;

                // Open the image file as a stream
                using (FileStream imgStream = File.OpenRead(imageFiles[i]))
                {
                    // Add the image to the newly created page, covering the entire page area
                    pdfMend.AddImage(
                        imgStream,          // image stream
                        pageNumber,         // target page
                        0f,                 // lower‑left X
                        0f,                 // lower‑left Y
                        pageWidth,          // upper‑right X
                        pageHeight);        // upper‑right Y
                }
            }

            // Save the assembled PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}
