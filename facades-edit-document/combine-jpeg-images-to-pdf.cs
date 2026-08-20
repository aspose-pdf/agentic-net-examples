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

        // Create a new PDF document
        using (Document pdfDoc = new Document())
        {
            // PdfFileMend works on an existing Document instance
            PdfFileMend pdfMend = new PdfFileMend(pdfDoc);

            // Add each image on a separate page
            for (int i = 0; i < imageFiles.Length; i++)
            {
                // Add a blank page to the document
                pdfDoc.Pages.Add();

                // Page numbers in Aspose.Pdf are 1‑based
                int pageNumber = i + 1;
                Page page = pdfDoc.Pages[pageNumber];

                // Determine page dimensions (width & height in points)
                // PageInfo properties are double, cast to float for AddImage
                float pageWidth  = (float)page.PageInfo.Width;
                float pageHeight = (float)page.PageInfo.Height;

                // Open the image stream and place it to fill the entire page
                using (FileStream imgStream = File.OpenRead(imageFiles[i]))
                {
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
