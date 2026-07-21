using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;
using System.Drawing.Imaging;

class ImageExtractor
{
    static void Main()
    {
        // Path to the source PDF file
        const string inputPdf = "input.pdf";
        // Directory where extracted images will be saved
        const string outputDir = "ExtractedImages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // ---------------------------------------------------------------------
        // Create a sample PDF with an embedded image so the example can run in a
        // sandbox that has no pre‑existing files.
        // ---------------------------------------------------------------------
        CreateSamplePdfWithImage(inputPdf);

        // Use PdfExtractor (Facade) to extract images
        using (PdfExtractor extractor = new PdfExtractor())
        {
            // Bind the PDF document to the extractor
            extractor.BindPdf(inputPdf);

            // Prepare the extractor for image extraction
            extractor.ExtractImage();

            // Iterate through all images in the PDF
            while (extractor.HasNextImage())
            {
                // Generate a new GUID for the image file name to avoid collisions
                string guid = Guid.NewGuid().ToString();

                // Build the full output file path (preserve original image format)
                string outputPath = Path.Combine(outputDir, guid + ".png");

                // Extract the next image and save it. Using the overload without
                // ImageFormat avoids the CA1416 platform warning.
                extractor.GetNextImage(outputPath);
            }
        }

        Console.WriteLine("Image extraction completed.");
    }

    /// <summary>
    /// Generates a minimal PDF containing a single PNG image. This method is used
    /// only for demonstration purposes so the example can run without external
    /// files.
    /// </summary>
    private static void CreateSamplePdfWithImage(string path)
    {
        // Create a simple bitmap in memory (red square)
        using (Bitmap bmp = new Bitmap(100, 100))
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // Fully qualify System.Drawing.Color to avoid ambiguity with Aspose.Pdf.Color
                g.Clear(System.Drawing.Color.Red);
            }

            // Save bitmap to a memory stream as PNG
            using (MemoryStream imgStream = new MemoryStream())
            {
                bmp.Save(imgStream, ImageFormat.Png);
                imgStream.Position = 0;

                // Create a new PDF document and add the image
                Document doc = new Document();
                Page page = doc.Pages.Add();
                Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image
                {
                    ImageStream = imgStream
                };
                page.Paragraphs.Add(pdfImage);

                // Save the PDF to the specified path
                doc.Save(path);
            }
        }
    }
}
