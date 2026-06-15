using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string imagePath = "logo.png";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF to obtain page dimensions (using the core API)
        using (Document doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Retrieve page width and height (points)
            double pageWidth  = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Desired image size (you can adjust as needed)
            const float imageWidth  = 200f;
            const float imageHeight = 100f;

            // Calculate coordinates to center the image on the page
            float lowerLeftX = (float)((pageWidth  - imageWidth)  / 2);
            float lowerLeftY = (float)((pageHeight - imageHeight) / 2);
            float upperRightX = lowerLeftX + imageWidth;
            float upperRightY = lowerLeftY + imageHeight;

            // Use PdfFileMend (Facade) to add the image with the calculated coordinates
            using (PdfFileMend mend = new PdfFileMend())
            {
                // Bind the source PDF
                mend.BindPdf(inputPdf);

                // Add the image to the first page using the computed rectangle
                using (FileStream imgStream = File.OpenRead(imagePath))
                {
                    mend.AddImage(imgStream, 1, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
                }

                // Save the modified PDF
                mend.Save(outputPdf);
                mend.Close();
            }

            Console.WriteLine($"Image placed and saved to '{outputPdf}'.");
        }
    }
}