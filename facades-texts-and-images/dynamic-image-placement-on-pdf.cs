using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "logo.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document to obtain page dimensions
        using (Document doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing
            Page page = doc.Pages[1];

            // Retrieve page width and height (points)
            double pageWidth  = page.Rect.Width;
            double pageHeight = page.Rect.Height;

            // Define image size as a fraction of the page size
            double imgWidth  = pageWidth  * 0.2; // 20% of page width
            double imgHeight = pageHeight * 0.2; // 20% of page height

            // Define a margin from the page edges
            double margin = 20.0; // points

            // Calculate lower‑left and upper‑right coordinates for the image rectangle
            double lowerLeftX  = pageWidth  - imgWidth - margin;
            double lowerLeftY  = margin;
            double upperRightX = pageWidth  - margin;
            double upperRightY = margin + imgHeight;

            // Initialize PdfFileMend facade, bind the source PDF, add the image, and save
            PdfFileMend mend = new PdfFileMend();
            mend.BindPdf(inputPdf);

            // Add image to page 1 using calculated coordinates
            mend.AddImage(imagePath, 1,
                         (float)lowerLeftX,
                         (float)lowerLeftY,
                         (float)upperRightX,
                         (float)upperRightY);

            // Save the modified PDF
            mend.Save(outputPdf);
            mend.Close();
        }

        Console.WriteLine($"Image placed and saved to '{outputPdf}'.");
    }
}