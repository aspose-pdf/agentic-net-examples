using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class CombineImagesToPdf
{
    static void Main(string[] args)
    {
        // Input: list of image file paths (can be passed as arguments or hard‑coded for demo)
        string[] imagePaths = new string[]
        {
            "image1.jpg",
            "image2.png",
            "image3.bmp"
        };

        // Output PDF file
        string outputPdf = "CombinedImages.pdf";

        // Validate that all image files exist before proceeding
        foreach (string imgPath in imagePaths)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Error: Image file not found – {imgPath}");
                return;
            }
        }

        try
        {
            // Create a new blank PDF document
            Document pdfDocument = new Document();

            // Process each image and add it to a new page
            foreach (string imgPath in imagePaths)
            {
                // Add a new page to the document
                Page page = pdfDocument.Pages.Add();

                // Load the image into a stream (kept open until the document is saved)
                FileStream imgStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read);
                Image img = new Image
                {
                    ImageStream = imgStream,
                    // Optional: scale the image to fit the page width while preserving aspect ratio
                    // Here we set the width to the page width minus a small margin
                    FixWidth = page.PageInfo.Width - 20
                };

                // Add the image to the page's paragraphs collection
                page.Paragraphs.Add(img);
            }

            // Save the assembled PDF
            pdfDocument.Save(outputPdf); // document-save rule

            Console.WriteLine($"Successfully created PDF '{outputPdf}' with {imagePaths.Length} images.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}