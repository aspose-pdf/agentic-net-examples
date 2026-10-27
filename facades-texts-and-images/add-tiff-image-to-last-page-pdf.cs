using System;
using System.IO;
using Aspose.Pdf;                     // Document, Page collection, etc.
using Aspose.Pdf.Facades;            // PdfFileMend

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string imagePath = "image.tif";
        const string outputPdf = "output.pdf";

        // Verify files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"TIFF image not found: {imagePath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPdf))
            {
                // Determine the last page number (Aspose.Pdf uses 1‑based indexing)
                int lastPageNumber = doc.Pages.Count;

                // Prepare the PdfFileMend helper (does NOT implement IDisposable)
                Aspose.Pdf.Facades.PdfFileMend mend = new Aspose.Pdf.Facades.PdfFileMend();

                // Bind the Document to the Mend object
                mend.BindPdf(doc);

                // Add the TIFF image to the last page.
                // The overload expects an int[] of page numbers.
                // Coordinates are given in points (1/72 inch). Adjust as needed.
                int[] targetPages = new int[] { lastPageNumber };
                float llx = 100f; // lower‑left X
                float lly = 500f; // lower‑left Y
                float urx = 300f; // upper‑right X
                float ury = 650f; // upper‑right Y

                mend.AddImage(imagePath, targetPages, llx, lly, urx, ury);

                // Save the modified PDF to the output path
                mend.Save(outputPdf);
            }

            Console.WriteLine($"TIFF image added to last page. Output saved as '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}