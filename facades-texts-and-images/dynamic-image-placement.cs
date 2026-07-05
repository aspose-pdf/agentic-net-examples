using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string imagePath = "logo.png";       // image to place
        const string outputPdf = "output.pdf";     // result PDF

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

        // Load the PDF document (lifecycle rule)
        using (Document doc = new Document(inputPdf))
        {
            // Access the first page to obtain its dimensions
            Page page = doc.Pages[1]; // 1‑based indexing (global rule)
            float pageWidth  = (float)page.Rect.Width;   // cast from double to float
            float pageHeight = (float)page.Rect.Height;  // cast from double to float

            // Define image size as a percentage of the page size (e.g., 30%)
            float imgWidth  = pageWidth  * 0.30f;
            float imgHeight = pageHeight * 0.30f;

            // Position the image with a margin of 10% from left and bottom edges
            float marginX = pageWidth  * 0.10f;
            float marginY = pageHeight * 0.10f;

            float lowerLeftX  = marginX;
            float lowerLeftY  = marginY;
            float upperRightX = marginX + imgWidth;
            float upperRightY = marginY + imgHeight;

            // Initialize PdfFileMend facade on the loaded document (lifecycle rule)
            PdfFileMend mender = new PdfFileMend(doc);

            // Add the image to page 1 using the calculated coordinates
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                bool success = mender.AddImage(
                    imgStream,
                    1,                 // page number (1‑based)
                    lowerLeftX,
                    lowerLeftY,
                    upperRightX,
                    upperRightY);

                if (!success)
                {
                    Console.Error.WriteLine("Failed to add image to the PDF.");
                }
            }

            // Close the facade (required to flush changes)
            mender.Close();

            // Save the modified PDF (save rule)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Image placed and PDF saved to '{outputPdf}'.");
    }
}
