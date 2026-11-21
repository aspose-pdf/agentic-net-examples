using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string tiffImage = "image.tif";

        // Verify that source files exist
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

        // Get the total number of pages using PdfFileInfo (PdfFileMend has no GetPageCount)
        int lastPage;
        using (PdfFileInfo info = new PdfFileInfo(inputPdf))
        {
            lastPage = info.NumberOfPages; // last page index (1‑based)
        }

        // PdfFileMend implements IDisposable – wrap it in a using block
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the existing PDF document
            mend.BindPdf(inputPdf);

            // Define the rectangle where the image will be placed
            // (lower‑left x/y and upper‑right x/y in points). PdfFileMend.AddImage expects float values.
            float llx = 100f; // lower‑left X
            float lly = 100f; // lower‑left Y
            float urx = 300f; // upper‑right X
            float ury = 300f; // upper‑right Y

            // Add the TIFF image to the last page without altering existing content
            // AddImage expects a Stream, so open the image file as a FileStream
            using (FileStream imgStream = File.OpenRead(tiffImage))
            {
                mend.AddImage(imgStream, lastPage, llx, lly, urx, ury);
            }

            // Save the modified PDF to a new file
            mend.Save(outputPdf);
        }

        Console.WriteLine($"TIFF image added to the last page. Output saved as '{outputPdf}'.");
    }
}
