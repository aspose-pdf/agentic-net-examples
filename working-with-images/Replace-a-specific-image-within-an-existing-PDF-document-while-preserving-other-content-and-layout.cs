using System;
using System.IO;
using Aspose.Pdf;

class ReplaceImageInPdf
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";          // Existing PDF
        const string newImagePath  = "newImage.jpg";      // Image to replace with (JPEG)
        const string outputPdfPath = "output.pdf";        // Resulting PDF

        // Verify input files exist
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

        try
        {
            // Load the PDF document (lifecycle rule: use using for disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Choose the page that contains the image to replace.
                // Here we use the first page; adjust the index as needed (1‑based).
                Aspose.Pdf.Page page = pdfDoc.Pages[1];

                // Access the image collection of the page.
                // XImageCollection does NOT support dictionary access; use index.
                var images = page.Resources.Images;

                // Ensure there is at least one image on the page.
                if (images.Count == 0)
                {
                    Console.Error.WriteLine("No images found on the selected page.");
                }
                else
                {
                    // Determine which image to replace.
                    // For demonstration we replace the first image (index = 1, 1‑based).
                    int imageIndexToReplace = 1;

                    // Open a stream for the new image (must be JPEG as per Replace method docs).
                    using (FileStream newImgStream = File.OpenRead(newImagePath))
                    {
                        // Replace the image in the collection.
                        // This keeps the original image resource reference (same index) so layout is preserved.
                        images.Replace(imageIndexToReplace, newImgStream);
                    }

                    Console.WriteLine($"Replaced image #{imageIndexToReplace} on page 1 with '{newImagePath}'.");
                }

                // Save the modified PDF (preserves all other content and layout).
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}