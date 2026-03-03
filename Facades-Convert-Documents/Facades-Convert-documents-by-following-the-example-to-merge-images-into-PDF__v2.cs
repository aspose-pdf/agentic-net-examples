using System;
using System.IO;
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
            "image3.tif"
        };

        // Output PDF file
        const string outputPdf = "merged_images.pdf";

        // Verify that at least one image exists
        bool anyExists = false;
        foreach (string img in imageFiles)
        {
            if (File.Exists(img))
            {
                anyExists = true;
                break;
            }
        }

        if (!anyExists)
        {
            Console.Error.WriteLine("No input images were found.");
            return;
        }

        // PdfFileMend is a Facade class that can create a new PDF and add images to it.
        // It implements IDisposable, so we wrap it in a using block.
        using (PdfFileMend pdfMend = new PdfFileMend())
        {
            int pageNumber = 1; // Aspose.Pdf uses 1‑based page indexing

            foreach (string imgPath in imageFiles)
            {
                if (!File.Exists(imgPath))
                {
                    Console.Error.WriteLine($"Image not found and will be skipped: {imgPath}");
                    continue;
                }

                // Add the image to a new page.
                // The coordinates are given in points (1/72 inch). Here we place the image
                // starting at the lower‑left corner (0,0) and let it occupy a rectangle
                // of 500x700 points. Adjust as needed for your page size.
                pdfMend.AddImage(imgPath, pageNumber, 0, 0, 500, 700);

                // Increment page number for the next image
                pageNumber++;
            }

            // Save the resulting PDF. PdfFileMend.Save(string) writes the PDF to disk.
            pdfMend.Save(outputPdf);
        }

        Console.WriteLine($"Images have been merged into PDF: {outputPdf}");
    }
}