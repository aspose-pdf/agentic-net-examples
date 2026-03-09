using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output PDF files
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "relocated_output.pdf";

        // Path to the image that will be relocated (must be the same image that already exists in the PDF)
        const string imagePath = "image_to_move.jpg";

        // Page number (1‑based) where the image resides
        const int pageNumber = 1;

        // New position for the image (coordinates are in points; 1 point = 1/72 inch)
        // lower‑left corner (x, y) and upper‑right corner (x, y)
        const float newLowerLeftX = 100f;
        const float newLowerLeftY = 150f;
        const float newUpperRightX = 300f;
        const float newUpperRightY = 350f;

        // Index of the image to be removed on the specified page (1‑based)
        // For demonstration we assume the first image on the page; adjust as needed.
        int[] imageIndexesToDelete = new int[] { 1 };

        // Ensure the input PDF exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the image file exists (required for re‑adding)
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // -----------------------------------------------------------------
            // STEP 1: Delete the existing image (graphic element) from the page
            // -----------------------------------------------------------------
            // PdfContentEditor provides DeleteImage overloads.
            // It works on the bound document; no need to create a separate Document instance.
            using (PdfContentEditor contentEditor = new PdfContentEditor())
            {
                contentEditor.BindPdf(pdfDoc);
                // Delete the specified image(s) on the given page.
                // The method expects the page number and an array of image indexes.
                contentEditor.DeleteImage(pageNumber, imageIndexesToDelete);
                // No explicit Save() is required here; changes are applied to the bound document.
            }

            // -----------------------------------------------------------------
            // STEP 2: Add the same image back at the new location
            // -----------------------------------------------------------------
            // PdfFileMend is used to add images to a PDF.
            using (PdfFileMend fileMend = new PdfFileMend())
            {
                fileMend.BindPdf(pdfDoc);
                // AddImage(string imagePath, int pageNum, float llx, float lly, float urx, float ury)
                // All coordinate arguments must be of type float (use the 'f' suffix).
                bool added = fileMend.AddImage(
                    imagePath,
                    pageNumber,
                    newLowerLeftX,
                    newLowerLeftY,
                    newUpperRightX,
                    newUpperRightY);

                if (!added)
                {
                    Console.Error.WriteLine("Failed to add the image at the new location.");
                }
            }

            // -----------------------------------------------------------------
            // STEP 3: Save the modified PDF
            // -----------------------------------------------------------------
            // Use the standard Save(string) method; no SaveOptions are needed for PDF output.
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Graphic element relocated and saved to '{outputPdfPath}'.");
    }
}