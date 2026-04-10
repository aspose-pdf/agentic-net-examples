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
        const string tiffImage = "image.tiff";

        // Verify that source files exist.
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

        // Bind the PDF to the PdfFileMend facade.
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdf);

        // Determine the last page number (Aspose.Pdf uses 1‑based indexing).
        int lastPage;
        using (Document doc = new Document(inputPdf))
        {
            lastPage = doc.Pages.Count;
        }

        // Add the TIFF image to the last page.
        // Coordinates are in points (1 point = 1/72 inch).
        // Example places the image in a rectangle from (50,50) to (200,200).
        mend.AddImage(tiffImage, lastPage, 50f, 50f, 200f, 200f);

        // Save the modified PDF.
        mend.Save(outputPdf);
        mend.Close();

        Console.WriteLine($"TIFF image added to page {lastPage}. Saved as '{outputPdf}'.");
    }
}
