using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source images (JPEG, PNG, BMP, GIF)
        string[] imageFiles = {
            "image1.jpg",
            "image2.png",
            "image3.bmp",
            "image4.gif"
        };

        const string outputPdf = "combined.pdf";

        // Create an empty PDF document and ensure it is disposed properly
        using (Document pdfDoc = new Document())
        {
            // Initialize PdfFileMend with the empty document
            PdfFileMend pdfMend = new PdfFileMend(pdfDoc);

            foreach (string imgPath in imageFiles)
            {
                if (!File.Exists(imgPath))
                {
                    Console.Error.WriteLine($"File not found: {imgPath}");
                    continue;
                }

                // Add a new page for the current image
                pdfDoc.Pages.Add();

                // Get the newly added page (last page in the collection)
                int pageNumber = pdfDoc.Pages.Count;
                Page page = pdfDoc.Pages[pageNumber];

                // Define the rectangle that covers the whole page
                float lowerLeftX = 0f;
                float lowerLeftY = 0f;
                // PageInfo.Width and Height are double; cast to float for AddImage
                float upperRightX = (float)page.PageInfo.Width;
                float upperRightY = (float)page.PageInfo.Height;

                // Insert the image, scaling it to fill the page
                // The method returns a bool indicating success; ignore for brevity
                pdfMend.AddImage(imgPath, pageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            }

            // Save the combined PDF document
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Combined PDF saved to '{outputPdf}'.");
    }
}
