using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output.pdf";
        const string tiffImagePath  = "stamp.tiff";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(tiffImagePath))
        {
            Console.Error.WriteLine($"TIFF image not found: {tiffImagePath}");
            return;
        }

        // Determine the last page number (1‑based indexing)
        int lastPageNumber;
        using (Document doc = new Document(inputPdfPath))
        {
            lastPageNumber = doc.Pages.Count;
        }

        // Add the TIFF image to the last page without altering existing content
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the source PDF
            mend.BindPdf(inputPdfPath);

            // Define image rectangle (lower‑left X/Y, upper‑right X/Y)
            // Adjust these values as needed for positioning and size
            float llx = 50f;   // lower‑left X
            float lly = 50f;   // lower‑left Y
            float urx = 250f;  // upper‑right X
            float ury = 250f;  // upper‑right Y

            // Add the image to the last page
            mend.AddImage(tiffImagePath, lastPageNumber, llx, lly, urx, ury);

            // Save the modified PDF
            mend.Save(outputPdfPath);
        }

        Console.WriteLine($"TIFF image added to page {lastPageNumber}. Output saved to '{outputPdfPath}'.");
    }
}