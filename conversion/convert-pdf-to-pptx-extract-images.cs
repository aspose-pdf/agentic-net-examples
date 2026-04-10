using System;
using System.IO;
using System.Drawing.Imaging;
using Aspose.Pdf; // PDF handling, conversion and image extraction

class Program
{
    static void Main()
    {
        // Input PDF, intermediate PPTX, and folder for extracted images
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string imagesDir = "ExtractedImages";

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // -------------------------------------------------
        // 1. Convert PDF to PPTX using Aspose.Pdf
        // -------------------------------------------------
        using (Document pdfDoc = new Document(pdfPath)) // Load PDF (lifecycle rule)
        {
            // Save the PDF directly as PPTX – no extra SaveOptions class is required
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);

            // -------------------------------------------------
            // 2. Extract images from the original PDF (Aspose.Slides is not referenced)
            // -------------------------------------------------
            Directory.CreateDirectory(imagesDir);
            int imageCounter = 1;

            foreach (Page page in pdfDoc.Pages)
            {
                // page.Resources.Images is a collection of XImage objects
                foreach (XImage pdfImage in page.Resources.Images)
                {
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        // Save the image to a memory stream in PNG format
                        pdfImage.Save(imgStream, ImageFormat.Png);
                        imgStream.Position = 0;

                        string imageFileName = $"page_{page.Number}_img_{imageCounter}.png";
                        string imagePath = Path.Combine(imagesDir, imageFileName);
                        File.WriteAllBytes(imagePath, imgStream.ToArray());
                        imageCounter++;
                    }
                }
            }
        }

        Console.WriteLine("PDF successfully converted to PPTX and images extracted.");
    }
}
