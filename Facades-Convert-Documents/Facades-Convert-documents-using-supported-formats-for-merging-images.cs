using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class MergeImagesToPdf
{
    static void Main(string[] args)
    {
        // Expected arguments: output PDF path followed by one or more image file paths
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: MergeImagesToPdf <output.pdf> <image1> [image2 ...]");
            return;
        }

        string outputPdfPath = args[0];
        string[] imagePaths = args.Skip(1).ToArray();

        // Verify that all image files exist
        foreach (var imgPath in imagePaths)
        {
            if (!File.Exists(imgPath))
            {
                Console.Error.WriteLine($"Image file not found: {imgPath}");
                return;
            }
        }

        try
        {
            // Create an empty PDF document
            Document pdfDocument = new Document();

            // Remove the default empty page that Aspose.Pdf creates
            if (pdfDocument.Pages.Count > 0)
                pdfDocument.Pages.Delete(1);

            // Use PdfFileMend facade to add images to the document
            using (PdfFileMend mend = new PdfFileMend(pdfDocument))
            {
                foreach (var imgPath in imagePaths)
                {
                    // Add a new blank page for each image
                    pdfDocument.Pages.Add();
                    int pageNumber = pdfDocument.Pages.Count; // 1‑based index of the newly added page

                    // Retrieve page dimensions (default is A4)
                    Page page = pdfDocument.Pages[pageNumber];
                    float pageWidth = (float)page.PageInfo.Width;
                    float pageHeight = (float)page.PageInfo.Height;

                    // Add the image to cover the entire page
                    mend.AddImage(imgPath, pageNumber, 0, 0, pageWidth, pageHeight);
                }
            }

            // Save the merged PDF
            pdfDocument.Save(outputPdfPath);
            Console.WriteLine($"Merged PDF created successfully at: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}