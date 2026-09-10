using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Open the PDF document
        using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(inputPdfPath))
        {
            int imageIndex = 1;

            // Iterate through each page
            foreach (Aspose.Pdf.Page page in pdfDoc.Pages)
            {
                // Iterate over all XImage resources on the page
                foreach (Aspose.Pdf.XImage xImg in page.Resources.Images)
                {
                    // Build a unique file name for each extracted image
                    string outputPath = Path.Combine(outputFolder, $"image_{imageIndex}.png");

                    // Save the original image bytes as PNG, preserving its native resolution
                    using (FileStream outStream = new FileStream(outputPath, FileMode.Create))
                    {
                        // XImage.Save requires a System.Drawing.Imaging.ImageFormat
                        xImg.Save(outStream, System.Drawing.Imaging.ImageFormat.Png);
                    }

                    imageIndex++;
                }
            }
        }

        Console.WriteLine("All images have been extracted successfully.");
    }
}