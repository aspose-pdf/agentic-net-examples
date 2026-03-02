using System;
using System.IO;
using Aspose.Pdf;
using System.Drawing; // required for System.Drawing.Image used by Watermark

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "watermarked.pdf";
        const string watermarkImgPath = "watermark.png";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(watermarkImgPath))
        {
            Console.Error.WriteLine($"Watermark image not found: {watermarkImgPath}");
            return;
        }

        // Load the watermark image using System.Drawing.Image (required by Watermark ctor)
        using (System.Drawing.Image sysImage = System.Drawing.Image.FromFile(watermarkImgPath))
        {
            // Create a Watermark object from the System.Drawing.Image
            Watermark watermark = new Watermark(sysImage);

            // Open the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Apply the same watermark to every page in the document
                foreach (Page page in pdfDoc.Pages)
                {
                    page.Watermark = watermark;
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Watermarked PDF saved to '{outputPdfPath}'.");
    }
}