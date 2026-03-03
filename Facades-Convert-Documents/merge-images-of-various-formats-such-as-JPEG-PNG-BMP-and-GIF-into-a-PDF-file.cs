using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input image files (any supported format: JPEG, PNG, BMP, GIF)
        List<string> imageFiles = new List<string>
        {
            "image1.jpg",
            "image2.png",
            "image3.bmp",
            "image4.gif"
        };

        const string outputPdf = "merged_images.pdf";

        // Verify that all image files exist before processing
        foreach (string imgPath in imageFiles)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image file not found: {imgPath}");
                return;
            }
        }

        // Create a new PDF document and ensure it is disposed properly
        using (Document pdfDoc = new Document())
        {
            // PdfFileMend works on an existing Document instance
            using (PdfFileMend mend = new PdfFileMend(pdfDoc))
            {
                foreach (string imgPath in imageFiles)
                {
                    // Add a blank page for each image
                    Page page = pdfDoc.Pages.Add();

                    // Determine page dimensions (default size is A4)
                    float pageWidth  = (float)page.PageInfo.Width;
                    float pageHeight = (float)page.PageInfo.Height;

                    // Open the image as a stream and place it on the newly added page
                    using (FileStream imgStream = File.OpenRead(imgPath))
                    {
                        // AddImage(stream, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY)
                        // Here we map the image to the full page area.
                        mend.AddImage(imgStream,
                                      page.Number,
                                      0,               // lower left X
                                      0,               // lower left Y
                                      pageWidth,       // upper right X
                                      pageHeight);     // upper right Y
                    }
                }
            }

            // Save the resulting PDF (using the standard Save method)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Images merged into PDF: {outputPdf}");
    }
}