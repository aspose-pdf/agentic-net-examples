using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ReplaceLowResImages
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string highResFolder  = "HighResImages"; // folder containing replacement PNGs

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

        // Load the PDF document (lifecycle rule: wrap in using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Facade for editing page content (required by the task)
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(pdfDoc);

            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= pdfDoc.Pages.Count; pageNumber++)
            {
                // Each page has an XImageCollection (Resources.Images)
                var imageCollection = pdfDoc.Pages[pageNumber].Resources.Images;

                // ImageCollection is 1‑based as well
                int imageCount = imageCollection.Count;

                for (int imageIndex = 1; imageIndex <= imageCount; imageIndex++)
                {
                    // Build the expected high‑resolution PNG file name.
                    // Example naming convention: "page{page}_img{index}.png"
                    string highResImagePath = Path.Combine(
                        highResFolder,
                        $"page{pageNumber}_img{imageIndex}.png");

                    // Replace only if the high‑resolution file exists
                    if (File.Exists(highResImagePath))
                    {
                        // ReplaceImage is a Facade method that swaps the image at the given
                        // page number and image index with the supplied file.
                        editor.ReplaceImage(pageNumber, imageIndex, highResImagePath);
                    }
                }
            }

            // Save the modified PDF (save rule: direct Save for PDF format)
            pdfDoc.Save(outputPdfPath);

            // Close the editor (optional, but releases internal resources)
            editor.Close();
        }

        Console.WriteLine($"Low‑resolution images replaced. Output saved to '{outputPdfPath}'.");
    }
}