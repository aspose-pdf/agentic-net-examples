using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string stampImage = "logo.png";
        const string outputPdf = "output.pdf";

        // Verify that the source PDF and the image exist
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

        // Load the PDF document (lifecycle: create/load)
        using (Document pdfDoc = new Document(inputPdf))
        {
            // Create an ImageStamp with a fixed size.
            // Width and Height are set in points and do not depend on the page size.
            ImageStamp imgStamp = new ImageStamp(stampImage);
            imgStamp.Width = 100;   // Fixed width (points)
            imgStamp.Height = 50;   // Fixed height (points)

            // Center the stamp on each page regardless of page dimensions.
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment = VerticalAlignment.Center;

            // Apply the same stamp to every page.
            foreach (Page page in pdfDoc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF (lifecycle: save)
            pdfDoc.Save(outputPdf);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPdf}'.");
    }
}