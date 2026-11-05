using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImageInPdf
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string newImagePath = "newImage.jpg";

        // Page number to process (1‑based as required by Aspose.Pdf)
        const int pageNumber = 1;

        // Validate files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(newImagePath))
        {
            Console.Error.WriteLine($"Replacement image not found: {newImagePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Ensure the requested page exists
            if (pageNumber < 1 || pageNumber > pdfDoc.Pages.Count)
            {
                Console.Error.WriteLine($"Page {pageNumber} is out of range. Document has {pdfDoc.Pages.Count} pages.");
                return;
            }

            // Get the target page (1‑based indexing)
            Page page = pdfDoc.Pages[pageNumber];

            // Replace the first raster image on the page while preserving its layout.
            // The actual placed images are represented by Aspose.Pdf.Image objects in the page's Paragraphs collection.
            bool replaced = false;
            foreach (var paragraph in page.Paragraphs)
            {
                if (paragraph is Image img)
                {
                    // Preserve position, size, rotation etc. – only change the source file.
                    img.File = newImagePath; // Assign new image file; Aspose.Pdf updates the XObject internally.
                    replaced = true;
                    break; // Remove this break if you need to replace multiple images.
                }
            }

            if (!replaced)
            {
                Console.Error.WriteLine("No images found on the specified page.");
                return;
            }

            // Save the modified PDF. The using block ensures the document stays alive until Save completes.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Image replaced and PDF saved to '{outputPdfPath}'.");
    }
}
