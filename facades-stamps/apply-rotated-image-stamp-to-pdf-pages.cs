using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output_stamped.pdf";
        const string stampImage = "stamp.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the PDF document using the high‑level API (Document)
        Document pdfDocument = new Document(inputPdf);

        // Apply the rotated image stamp to each page
        for (int pageNum = 1; pageNum <= pdfDocument.Pages.Count; pageNum++)
        {
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                // Rotate the stamp 30 degrees
                RotateAngle = 30,

                // Align to bottom‑right corner
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,

                // Optional margins from the edges
                BottomMargin = 10,
                RightMargin  = 10
            };

            // Add the stamp to the current page
            pdfDocument.Pages[pageNum].AddStamp(imgStamp);
        }

        // Save the modified PDF
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
