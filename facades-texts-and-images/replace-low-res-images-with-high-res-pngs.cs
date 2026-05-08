using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output.pdf";
        const string highResDir = "HighResImages"; // folder containing high‑resolution PNGs

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Bind the PDF to the editor (facade) and work within a using block for deterministic disposal
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPdf);

            // Get total number of pages (1‑based indexing)
            int pageCount = editor.Document.Pages.Count;

            // Loop through each page
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Number of images on the current page (1‑based indexing)
                int imageCount = editor.Document.Pages[pageNum].Resources.Images.Count;

                // Loop through each image on the page
                for (int imgIdx = 1; imgIdx <= imageCount; imgIdx++)
                {
                    // Build the expected high‑resolution PNG file name.
                    // Example naming convention: page1_img1.png, page2_img3.png, etc.
                    string highResPath = Path.Combine(highResDir, $"page{pageNum}_img{imgIdx}.png");

                    // Replace only if the high‑resolution image exists
                    if (File.Exists(highResPath))
                    {
                        // Replace the low‑resolution image with the high‑resolution PNG
                        editor.ReplaceImage(pageNum, imgIdx, highResPath);
                    }
                }
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"All images replaced. Output saved to '{outputPdf}'.");
    }
}