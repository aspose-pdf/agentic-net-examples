using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF
        const string tiffImage = "image.tif";   // TIFF to add
        const string outputPdf = "output.pdf";  // result PDF

        // Validate files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(tiffImage))
        {
            Console.Error.WriteLine($"TIFF image not found: {tiffImage}");
            return;
        }

        // Determine the last page number and its dimensions
        int lastPageNumber;
        float pageWidth, pageHeight;
        using (Document doc = new Document(inputPdf))
        {
            // Aspose.Pdf uses 1‑based page indexing
            lastPageNumber = doc.Pages.Count;
            Page lastPage = doc.Pages[lastPageNumber];
            // PageInfo.Width/Height are double – cast to float for PdfFileMend
            pageWidth  = (float)lastPage.PageInfo.Width;
            pageHeight = (float)lastPage.PageInfo.Height;
        }

        // Add the TIFF image to the last page without affecting existing content
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdf);

        // Example placement: lower‑left corner, scaled to half the page size
        float lowerLeftX  = 0f;
        float lowerLeftY  = 0f;
        float upperRightX = pageWidth / 2f;
        float upperRightY = pageHeight / 2f;

        // AddImage(string imagePath, int pageNum, float llx, float lly, float urx, float ury)
        mend.AddImage(tiffImage, lastPageNumber, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

        // Save the modified PDF
        mend.Save(outputPdf);
        mend.Close();

        Console.WriteLine($"TIFF image added to page {lastPageNumber}. Saved as '{outputPdf}'.");
    }
}
