using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ReplaceImagesInPdf
{
    static void Main()
    {
        // Input PDF, replacement image, and output PDF paths
        const string inputPdfPath  = "input.pdf";
        const string replacementImagePath = "newImage.jpg";
        const string outputPdfPath = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(replacementImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {replacementImagePath}");
            return;
        }

        // Open the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // PdfContentEditor provides the ReplaceImage method (Facades API)
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the opened document to the editor
                editor.BindPdf(pdfDoc);

                // Iterate through all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    // Get the number of images on the current page
                    int imageCount = pdfDoc.Pages[pageNum].Resources.Images.Count;

                    // Replace each image on the page with the new image
                    // Image indexes are also 1‑based
                    for (int imgIndex = 1; imgIndex <= imageCount; imgIndex++)
                    {
                        // ReplaceImage(pageNumber, imageIndex, newImageFilePath)
                        editor.ReplaceImage(pageNum, imgIndex, replacementImagePath);
                    }
                }

                // Save the modified PDF to the output file
                editor.Save(outputPdfPath);
            }
        }

        Console.WriteLine($"Images replaced successfully. Output saved to '{outputPdfPath}'.");
    }
}