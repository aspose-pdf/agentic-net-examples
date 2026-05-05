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
            // add more image paths as needed
        };

        // Output PDF file
        const string outputPdf = "merged.pdf";

        // Verify that all image files exist before proceeding
        foreach (string img in imageFiles)
        {
            if (!File.Exists(img))
            {
                Console.Error.WriteLine($"Image file not found: {img}");
                return;
            }
        }

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document())
        {
            // Bind the document to PdfFileMend facade (also disposed via using)
            using (PdfFileMend pdfMend = new PdfFileMend(pdfDoc))
            {
                // Process each image
                foreach (string imgPath in imageFiles)
                {
                    // Add a blank page to the document
                    Page page = pdfDoc.Pages.Add();

                    // Determine the page number (1‑based indexing)
                    int pageNumber = pdfDoc.Pages.Count;

                    // Open the image file as a stream
                    using (FileStream imgStream = File.OpenRead(imgPath))
                    {
                        // Add the image to the newly created page.
                        // Coordinates (0,0) to (page width, page height) make the image fill the page.
                        pdfMend.AddImage(
                            imgStream,
                            pageNumber,
                            0f,                     // lower left X
                            0f,                     // lower left Y
                            (float)page.PageInfo.Width,   // upper right X (cast to float)
                            (float)page.PageInfo.Height   // upper right Y (cast to float)
                        );
                    }
                }
            }

            // Save the assembled PDF to the specified file
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"PDF created successfully: {outputPdf}");
    }
}
