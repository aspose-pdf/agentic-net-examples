using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for input PDF and output PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";

        // Path to the image file that will be loaded into a memory stream
        const string imageFilePath = "logo.png";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imageFilePath))
        {
            Console.Error.WriteLine($"Image file not found: {imageFilePath}");
            return;
        }

        // Load the image into a MemoryStream (this could also be any other stream source)
        using (FileStream imgFileStream = File.OpenRead(imageFilePath))
        using (MemoryStream imgMemoryStream = new MemoryStream())
        {
            imgFileStream.CopyTo(imgMemoryStream);
            imgMemoryStream.Position = 0; // reset stream position for reading

            // Create the ImageStamp from the memory stream
            ImageStamp imgStamp = new ImageStamp(imgMemoryStream)
            {
                // Example positioning and appearance settings
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center,
                Opacity             = 0.5,   // 50% transparent
                Background          = false  // stamp appears on top of page content
            };

            // Open the PDF document inside a using block (ensures deterministic disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];
                    // Add the image stamp to the current page
                    page.AddStamp(imgStamp);
                }

                // Save the modified PDF (no SaveOptions needed for PDF output)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Image stamp applied and saved to '{outputPdfPath}'.");
        }
    }
}