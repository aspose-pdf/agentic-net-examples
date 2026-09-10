using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "background.png";

        // Validate files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Background image not found: {imagePath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF to obtain page count and page rectangles.
        //    Aspose.Pdf uses 1‑based page indexing.
        // -----------------------------------------------------------------
        int pageCount;
        (double llx, double lly, double urx, double ury)[] pageRects;

        using (Document doc = new Document(inputPdf))
        {
            pageCount = doc.Pages.Count;
            pageRects = new (double, double, double, double)[pageCount];

            for (int i = 1; i <= pageCount; i++)
            {
                var rect = doc.Pages[i].Rect;
                pageRects[i - 1] = (rect.LLX, rect.LLY, rect.URX, rect.URY);
            }
        }

        // -----------------------------------------------------------------
        // 2. Initialise PdfFileMend without the obsolete destination ctor.
        //    Bind the source PDF, then add images, and finally Save.
        // -----------------------------------------------------------------
        PdfFileMend mender = new PdfFileMend();
        mender.BindPdf(inputPdf);

        // -----------------------------------------------------------------
        // 3. Iterate over all pages and add the background image.
        //    Use the stream overload of AddImage and cast coordinates to float.
        // -----------------------------------------------------------------
        for (int i = 1; i <= pageCount; i++)
        {
            var (llx, lly, urx, ury) = pageRects[i - 1];

            using (FileStream imgStream = File.OpenRead(imagePath))
            {
                mender.AddImage(
                    imgStream,
                    i,
                    (float)llx,
                    (float)lly,
                    (float)urx,
                    (float)ury);
            }
        }

        // -----------------------------------------------------------------
        // 4. Save the modified PDF and release resources.
        // -----------------------------------------------------------------
        mender.Save(outputPdf);
        mender.Close();

        Console.WriteLine($"Background image added to all {pageCount} pages. Output saved to '{outputPdf}'.");
    }
}
