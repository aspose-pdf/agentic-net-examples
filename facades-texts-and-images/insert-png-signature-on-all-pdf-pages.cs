using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and PNG signature image paths
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output_signed.pdf";
        const string pngImage  = "signature.png";

        // Verify that the required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pngImage))
        {
            Console.Error.WriteLine($"Signature image not found: {pngImage}");
            return;
        }

        // Use PdfFileMend facade to modify the PDF
        // The facade implements IDisposable, so wrap it in a using block
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Bind the source PDF file
            mend.BindPdf(inputPdf);

            // Retrieve the total number of pages (Aspose.Pdf uses 1‑based indexing)
            int pageCount = mend.Document.Pages.Count;

            // Build an array containing all page numbers
            int[] allPages = Enumerable.Range(1, pageCount).ToArray();

            // Define the rectangle where the image will be placed.
            // lowerLeftX, lowerLeftY = 0,0 (bottom‑left corner of the page)
            // upperRightX, upperRightY = 100,100 (image size 100×100 points)
            float lowerLeftX = 0f;
            float lowerLeftY = 0f;
            float upperRightX = 100f;
            float upperRightY = 100f;

            // Add the PNG image to every page at the specified coordinates
            mend.AddImage(pngImage, allPages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

            // Save the modified PDF to the output file
            mend.Save(outputPdf);
        }

        Console.WriteLine($"Signature image added to all pages. Output saved to '{outputPdf}'.");
    }
}