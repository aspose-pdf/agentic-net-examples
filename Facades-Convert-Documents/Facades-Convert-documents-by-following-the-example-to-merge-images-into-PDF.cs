using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class MergeImagesToPdf
{
    static void Main(string[] args)
    {
        // Input: array of image file paths to be merged
        string[] imageFiles = new string[]
        {
            "image1.jpg",
            "image2.png",
            "image3.bmp"
        };

        // Output PDF file
        string outputPdf = "merged.pdf";

        // Validate that all image files exist
        foreach (string imgPath in imageFiles)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Error: Image file not found – {imgPath}");
                return;
            }
        }

        try
        {
            // Create a new blank PDF document
            Document pdfDoc = new Document();

            // Facade for adding images to the PDF
            PdfFileMend mend = new PdfFileMend(pdfDoc);

            int pageNumber = 1;

            foreach (string imgPath in imageFiles)
            {
                // Ensure the target page exists; add a new page if necessary
                if (pageNumber > pdfDoc.Pages.Count)
                {
                    pdfDoc.Pages.Add();
                }

                // Get page dimensions (in points)
                Page page = pdfDoc.Pages[pageNumber];
                float pageWidth = (float)page.PageInfo.Width;
                float pageHeight = (float)page.PageInfo.Height;

                // Add the image to the current page, filling the whole page
                // Parameters: image path, page number, lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
                mend.AddImage(imgPath, pageNumber, 0, 0, pageWidth, pageHeight);

                pageNumber++;
            }

            // Save the resulting PDF
            pdfDoc.Save(outputPdf);
            Console.WriteLine($"Images merged successfully into '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}