using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class ImageToPdfMerger
{
    static void Main(string[] args)
    {
        // Input image files (can be supplied via command‑line arguments)
        // If no arguments are provided, use a sample list.
        string[] imageFiles = args.Length > 0
            ? args
            : new string[]
            {
                "sample1.jpg",
                "sample2.png",
                "sample3.bmp",
                "sample4.gif"
            };

        const string outputPdf = "MergedImages.pdf";

        try
        {
            // Create a new blank PDF document.
            Document pdfDocument = new Document();

            foreach (string imgPath in imageFiles)
            {
                if (!File.Exists(imgPath))
                {
                    Console.WriteLine($"Warning: Image file not found – {imgPath}");
                    continue; // skip missing files
                }

                // Add a new page for each image.
                Page page = pdfDocument.Pages.Add();

                // Load the image into a stream and keep the stream open until the document is saved.
                using (FileStream imgStream = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                {
                    // Create an Image object from the stream.
                    Image img = new Image
                    {
                        ImageStream = imgStream,
                        // Scale the image to fit the page while preserving aspect ratio.
                        // FixWidth/FixHeight are optional; setting both forces exact size.
                        // Here we fit the image to the page dimensions.
                        FixWidth = page.PageInfo.Width,
                        FixHeight = page.PageInfo.Height
                    };

                    // Add the image to the page's paragraph collection.
                    page.Paragraphs.Add(img);
                }
            }

            // Save the resulting PDF.
            pdfDocument.Save(outputPdf);
            Console.WriteLine($"PDF created successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}