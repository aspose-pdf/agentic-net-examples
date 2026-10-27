using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the JPEG images to be merged.
        string[] imagePaths = new string[]
        {
            "image1.jpg",
            "image2.jpg",
            "image3.jpg"
        };

        // Output PDF file.
        const string outputPdfPath = "merged_images.pdf";

        // Verify that all image files exist before processing.
        foreach (string imgPath in imagePaths)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image file not found: {imgPath}");
                return;
            }
        }

        // Create a new PDF document.
        var pdfDocument = new Document();

        // Add each image to a separate page.
        foreach (string imgPath in imagePaths)
        {
            // Add a new page to the document.
            var page = pdfDocument.Pages.Add();

            // Create an Aspose.Pdf.Image object and bind it to the file.
            var image = new Aspose.Pdf.Image { File = imgPath };

            // Optionally, you can set the page size to match the image dimensions.
            // This requires System.Drawing.Common (or any other way to read image size).
            // Uncomment the following block if you want the page size to match the image.
            //
            // using (var sysImg = System.Drawing.Image.FromFile(imgPath))
            // {
            //     page.SetPageSize(sysImg.Width, sysImg.Height);
            // }

            // Add the image to the page.
            page.Paragraphs.Add(image);
        }

        // Save the combined PDF.
        pdfDocument.Save(outputPdfPath);

        Console.WriteLine($"PDF successfully created at '{outputPdfPath}'.");
    }
}
