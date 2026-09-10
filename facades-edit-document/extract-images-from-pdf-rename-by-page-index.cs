using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        // -----------------------------------------------------------------
        // Step 1: Create a sample PDF with a couple of images (inline generation)
        // -----------------------------------------------------------------
        const string pdfPath = "sample.pdf";
        const string tempImagePath = "temp_image.png";

        // Create a simple PNG image using System.Drawing (Windows‑only, but acceptable for demo)
        // The image is a 100x100 red square.
        using (var bmp = new System.Drawing.Bitmap(100, 100))
        {
            using (var gfx = System.Drawing.Graphics.FromImage(bmp))
            {
                gfx.Clear(System.Drawing.Color.Red);
            }
            bmp.Save(tempImagePath, System.Drawing.Imaging.ImageFormat.Png);
        }

        // Build a PDF that contains the image on two pages
        using (Document doc = new Document())
        {
            // Page 1
            Page page1 = doc.Pages.Add();
            Image img1 = new Image
            {
                File = tempImagePath,
                // Position the image on the page (coordinates in points)
                // Rectangle(left, bottom, width, height)
                // Here we place it at (50, 500) with size 100x100
                // Use fully qualified type to avoid ambiguity
                // Note: Aspose.Pdf.Image does not have a constructor that takes a path
            };
            page1.Paragraphs.Add(img1);

            // Page 2 (same image)
            Page page2 = doc.Pages.Add();
            Image img2 = new Image { File = tempImagePath };
            page2.Paragraphs.Add(img2);

            // Save the PDF to disk
            doc.Save(pdfPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Extract images using PdfExtractor and rename them
        // -----------------------------------------------------------------
        // Load the PDF to obtain the page count
        using (Document srcDoc = new Document(pdfPath))
        {
            int totalPages = srcDoc.Pages.Count;

            // Initialize the extractor
            PdfExtractor extractor = new PdfExtractor();

            // Bind the PDF file
            extractor.BindPdf(pdfPath);

            // Iterate over each page to get page‑specific image indexes
            for (int pageNumber = 1; pageNumber <= totalPages; pageNumber++)
            {
                // Restrict extraction to the current page
                extractor.StartPage = pageNumber;
                extractor.EndPage   = pageNumber;

                // Perform the extraction for this page
                extractor.ExtractImage();

                int imageIndex = 1;
                while (extractor.HasNextImage())
                {
                    // Build the output file name: Image_Page{page}_Index{index}.png
                    string outputFile = $"Image_Page{pageNumber}_Index{imageIndex}.png";

                    // Save the image in PNG format
                    extractor.GetNextImage(outputFile, System.Drawing.Imaging.ImageFormat.Png);

                    imageIndex++;
                }
            }
        }

        // Cleanup temporary image file
        if (File.Exists(tempImagePath))
        {
            File.Delete(tempImagePath);
        }

        Console.WriteLine("Image extraction completed.");
    }
}