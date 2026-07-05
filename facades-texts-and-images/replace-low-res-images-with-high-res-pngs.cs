using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ReplaceLowResImages
{
    static void Main()
    {
        // Input PDF containing low‑resolution images
        const string inputPdfPath  = "input.pdf";
        // Output PDF with high‑resolution PNG replacements
        const string outputPdfPath = "output.pdf";
        // Path to the high‑resolution PNG that will replace each image
        const string highResPngPath = "highres.png";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(highResPngPath))
        {
            Console.Error.WriteLine($"High‑resolution image not found: {highResPngPath}");
            return;
        }

        // Load the PDF to obtain page and image counts (required for the loop)
        using (Document doc = new Document(inputPdfPath))
        {
            int pageCount = doc.Pages.Count; // 1‑based indexing

            // Facade for editing PDF content (image replacement)
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(inputPdfPath);

            // Iterate over each page
            for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
            {
                // Number of images on the current page (1‑based)
                int imageCount = doc.Pages[pageNumber].Resources.Images.Count;

                // Replace each image with the high‑resolution PNG
                for (int imageIndex = 1; imageIndex <= imageCount; imageIndex++)
                {
                    // ReplaceImage(page, index, newImageFile)
                    editor.ReplaceImage(pageNumber, imageIndex, highResPngPath);
                }
            }

            // Save the modified PDF
            editor.Save(outputPdfPath);
        }

        Console.WriteLine($"High‑resolution images applied. Output saved to '{outputPdfPath}'.");
    }
}