using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "photo.jpg";

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

        // Load the PDF document (core API)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create the image stamp from a FileStream
            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                ImageStamp imgStamp = new ImageStamp(imgStream)
                {
                    // Position the image at the top centre with a 3‑point offset from the top edge
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Top,
                    YIndent = 3,               // top margin (points)
                    Background = false        // place over existing content
                };

                // Apply the stamp to every page
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(imgStamp);
                }
            }

            // Save the stamped PDF
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}
