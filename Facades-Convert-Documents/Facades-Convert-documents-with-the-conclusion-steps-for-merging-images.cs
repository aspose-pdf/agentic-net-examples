using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF and images (adjust paths as needed)
        string inputPdfPath = "input.pdf";
        string outputPdfPath = "output.pdf";
        List<string> imagePaths = new List<string>
        {
            "image1.png",
            "image2.jpg"
        };

        // Validate input PDF
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found – {inputPdfPath}");
            return;
        }

        // Validate images
        foreach (var imgPath in imagePaths)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Error: Image file not found – {imgPath}");
                return;
            }
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Initialize PdfFileMend to add images
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(pdfDocument);

            // Add each image to a new page at the bottom of the document
            int pageNumber = pdfDocument.Pages.Count + 1;
            foreach (var imgPath in imagePaths)
            {
                // Create a new blank page for the image
                pdfDocument.Pages.Add();
                // Define placement rectangle (example: full page)
                // Coordinates are in points (1/72 inch). Here we use page size.
                var page = pdfDocument.Pages[pageNumber];
                float llx = 0;
                float lly = 0;
                float urx = (float)page.PageInfo.Width;
                float ury = (float)page.PageInfo.Height;

                using (FileStream imgStream = File.OpenRead(imgPath))
                {
                    // Add the image to the newly created page
                    mend.AddImage(imgStream, pageNumber, llx, lly, urx, ury);
                }

                pageNumber++;
            }

            // Save the modified PDF
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"PDF saved successfully to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}