using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string imageTiff = "image.tiff";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(imageTiff))
        {
            Console.Error.WriteLine($"TIFF image not found: {imageTiff}");
            return;
        }

        // Initialize PdfFileMend and bind the source PDF
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdf);

        // Get the number of the last page (pages are 1‑based)
        int lastPage = mend.Document.Pages.Count;

        // Add the TIFF image to the last page at the desired rectangle
        using (FileStream imgStream = File.OpenRead(imageTiff))
        {
            // lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            bool added = mend.AddImage(imgStream, lastPage, 50f, 50f, 200f, 200f);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add the image to the PDF.");
            }
        }

        // Save the modified PDF and release resources
        mend.Save(outputPdf);
        mend.Close();

        Console.WriteLine($"TIFF image added to page {lastPage}. Saved as '{outputPdf}'.");
    }
}
