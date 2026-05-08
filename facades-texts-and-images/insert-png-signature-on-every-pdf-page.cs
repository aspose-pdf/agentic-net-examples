using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;                 // Document class for page count
using Aspose.Pdf.Facades;        // PdfFileMend class

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";          // source PDF
        const string outputPdf     = "output_signed.pdf";  // result PDF
        const string signatureImg  = "signature.png";      // PNG to place on each page

        // Ensure the source files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(signatureImg))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImg}");
            return;
        }

        // Determine how many pages the document has (required for PdfFileMend)
        int pageCount;
        using (Document doc = new Document(inputPdf))
        {
            pageCount = doc.Pages.Count;   // Aspose.Pdf uses 1‑based indexing
        }

        // Build an array with all page numbers (1 … pageCount)
        int[] allPages = Enumerable.Range(1, pageCount).ToArray();

        // Create PdfFileMend, bind the source PDF, add the image to every page,
        // then save the modified document.
        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdf);

        // Coordinates for the bottom‑left corner.
        // lowerLeftX = 0, lowerLeftY = 0 (origin is bottom‑left of the page)
        // upperRightX/Y define the size of the image (e.g., 100×50 points)
        const float lowerLeftX = 0f;
        const float lowerLeftY = 0f;
        const float upperRightX = 100f;
        const float upperRightY = 50f;

        bool success = mend.AddImage(signatureImg, allPages, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
        if (!success)
        {
            Console.Error.WriteLine("Failed to add the signature image to the PDF.");
        }

        // Save the result and release resources.
        mend.Save(outputPdf);
        mend.Close();

        Console.WriteLine($"Signature image added to all pages. Output saved to '{outputPdf}'.");
    }
}