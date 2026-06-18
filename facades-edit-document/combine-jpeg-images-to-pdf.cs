using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input JPEG files – adjust the paths as needed
        string[] imageFiles = new string[]
        {
            "image1.jpg",
            "image2.jpg",
            "image3.jpg"
        };

        const string outputPdf = "combined.pdf";

        // Verify that all image files exist before proceeding
        foreach (string imgPath in imageFiles)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image not found: {imgPath}");
                return;
            }
        }

        // Create a new empty PDF document
        using (Document pdfDoc = new Document())
        {
            // PdfFileMend works on an existing Document instance
            using (PdfFileMend mend = new PdfFileMend(pdfDoc))
            {
                int pageNumber = 1; // Aspose.Pdf uses 1‑based page indexing

                foreach (string imgPath in imageFiles)
                {
                    // Add a blank page for the current image
                    pdfDoc.Pages.Add();

                    // Open the image as a stream
                    using (FileStream imgStream = File.OpenRead(imgPath))
                    {
                        // Retrieve the size of the newly added page
                        Page page = pdfDoc.Pages[pageNumber];
                        double pageWidth  = page.PageInfo.Width;
                        double pageHeight = page.PageInfo.Height;

                        // Place the image to fill the entire page
                        // Parameters: (imageStream, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY)
                        mend.AddImage(imgStream, pageNumber, 0, 0, (float)pageWidth, (float)pageHeight);
                    }

                    pageNumber++; // Move to the next page index
                }

                // Save the assembled PDF
                pdfDoc.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}