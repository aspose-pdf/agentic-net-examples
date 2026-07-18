using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "watermarked.pdf";
        const string watermarkImagePath = "watermark.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(watermarkImagePath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Apply the watermark to each page
            foreach (Aspose.Pdf.Page page in doc.Pages)
            {
                // Create an image stamp from the watermark file
                Aspose.Pdf.ImageStamp imgStamp = new Aspose.Pdf.ImageStamp(watermarkImagePath);

                // Scale the stamp to half the page size
                imgStamp.Width = page.PageInfo.Width / 2;
                imgStamp.Height = page.PageInfo.Height / 2;

                // Center the stamp on the page
                imgStamp.XIndent = (page.PageInfo.Width - imgStamp.Width) / 2;
                imgStamp.YIndent = (page.PageInfo.Height - imgStamp.Height) / 2;

                // Rotate the stamp by 45 degrees
                imgStamp.RotateAngle = 45;

                // Optional: set opacity (0.0 to 1.0)
                imgStamp.Opacity = 0.7f;

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}