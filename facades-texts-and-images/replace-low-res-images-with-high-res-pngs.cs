using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class ReplaceLowResImages
{
    static void Main()
    {
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
            Console.Error.WriteLine($"High‑resolution images folder not found: {highResFolder}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Facade for editing content (including image replacement)
            using (PdfContentEditor editor = new PdfContentEditor())
            {
                // Bind the document to the editor
                editor.BindPdf(doc);

                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Image collection on the page (1‑based indexing)
                    int imageCount = page.Resources.Images.Count;

                    // Loop over each image ID on the current page
                    for (int imgIndex = 1; imgIndex <= imageCount; imgIndex++)
                    {
                        // Build the expected high‑resolution PNG file name.
                        // Example naming convention: "page1_img1.png", "page2_img3.png", etc.
                        string highResFileName = $"page{pageNum}_img{imgIndex}.png";
                        string highResPath = Path.Combine(highResFolder, highResFileName);

                        // If a replacement image exists, replace the low‑resolution image
                        if (File.Exists(highResPath))
                        {
                            // ReplaceImage replaces the image at the given page and index
                            editor.ReplaceImage(pageNum, imgIndex, highResPath);
                            Console.WriteLine($"Replaced image {imgIndex} on page {pageNum} with '{highResFileName}'.");
                        }
                    }
                }

                // Save the modified document (PDF format)
                doc.Save(outputPdfPath);
                Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
            }
        }
    }
}