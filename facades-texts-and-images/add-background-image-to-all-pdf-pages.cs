using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_with_background.pdf";
        const string bgImage = "background.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(bgImage))
        {
            Console.Error.WriteLine($"Background image not found: {bgImage}");
            return;
        }

        // Use PdfFileMend facade to add images to existing PDF pages
        using (PdfFileMend mender = new PdfFileMend())
        {
            // Bind the source PDF file
            mender.BindPdf(inputPdf);

            // Get page count from the underlying Document
            int pageCount = mender.Document.Pages.Count;

            // Iterate through all pages (1‑based indexing)
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Retrieve page dimensions to place the image as full‑page background
                Page page = mender.Document.Pages[pageNum];
                float llx = (float)page.Rect.LLX;
                float lly = (float)page.Rect.LLY;
                float urx = (float)page.Rect.URX;
                float ury = (float)page.Rect.URY;

                // Open the image stream for each page
                using (FileStream imgStream = File.OpenRead(bgImage))
                {
                    // Add the image to the current page covering the whole page area
                    mender.AddImage(imgStream, pageNum, llx, lly, urx, ury);
                }
            }

            // Save the modified PDF to the output file
            mender.Save(outputPdf);
            // Close the facade (optional, using will dispose)
            mender.Close();
        }

        Console.WriteLine($"Background image added to all pages. Output saved to '{outputPdf}'.");
    }
}
