using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ReplaceLowResImages
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_replaced.pdf";
        const string highResFolder = "HighResImages"; // folder containing PNGs named like "<imageName>.png"

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!Directory.Exists(highResFolder))
        {
            Console.Error.WriteLine($"High‑resolution image folder not found: {highResFolder}");
            return;
        }

        try
        {
            // Load the PDF with the high‑level Document API – needed to enumerate images.
            Document pdfDoc = new Document(inputPdfPath);

            // Facade for low‑level image replacement.
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);

            int pageNumber = 1; // Aspose pages are 1‑based.
            foreach (Page page in pdfDoc.Pages)
            {
                // The Images collection is also 1‑based.
                var images = page.Resources.Images;
                for (int imgIdx = 1; imgIdx <= images.Count; imgIdx++)
                {
                    // Each Image object has a Name property (e.g., "Im1", "Im2", ...).
                    string imageName = images[imgIdx].Name;
                    string highResPath = Path.Combine(highResFolder, imageName + ".png");

                    if (File.Exists(highResPath))
                    {
                        // Replace the image on the current page at the given index.
                        editor.ReplaceImage(pageNumber, imgIdx, highResPath);
                        Console.WriteLine($"Replaced image '{imageName}' on page {pageNumber} (index {imgIdx}) with '{highResPath}'.");
                    }
                    else
                    {
                        Console.WriteLine($"No high‑res PNG for image '{imageName}' on page {pageNumber}; original retained.");
                    }
                }
                pageNumber++;
            }

            // Save the modified PDF.
            editor.Save(outputPdfPath);
            Console.WriteLine($"PDF saved with replaced images to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during image replacement: {ex.Message}");
        }
    }
}
