using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // for System.Drawing.Image

class Program
{
    static void Main()
    {
        // List of JPEG image files to be added to the PDF.
        string[] imageFiles = { "image1.jpg", "image2.jpg", "image3.jpg" };
        string outputPdfPath = "combined.pdf";

        // Verify that each image file exists before conversion.
        foreach (string img in imageFiles)
        {
            if (!File.Exists(img))
            {
                Console.Error.WriteLine($"File not found: {img}");
                return;
            }
        }

        try
        {
            // Create a new PDF document.
            Document pdfDoc = new Document();

            foreach (string imgPath in imageFiles)
            {
                // Load the image to obtain its dimensions using the fully‑qualified System.Drawing.Image.
                using (System.Drawing.Image sysImg = System.Drawing.Image.FromFile(imgPath))
                {
                    // Add a new page sized to the image.
                    Page page = pdfDoc.Pages.Add();
                    page.PageInfo.Width = sysImg.Width;
                    page.PageInfo.Height = sysImg.Height;

                    // Create an Aspose.Pdf.Image object and set the source file.
                    Aspose.Pdf.Image pdfImg = new Aspose.Pdf.Image
                    {
                        File = imgPath // Use the File property instead of the removed ImageInfo property.
                    };

                    // Add the image to the page.
                    page.Paragraphs.Add(pdfImg);
                }
            }

            // Save the combined PDF.
            pdfDoc.Save(outputPdfPath);
            Console.WriteLine($"PDF successfully created at '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}
