using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "output.pdf";     // PDF with signature image
        const string signatureImage = "signature.png"; // PNG to place on each page

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(signatureImage))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImage}");
            return;
        }

        // PdfFileMend works as a facade for adding images/text to existing PDFs.
        PdfFileMend mend = new PdfFileMend();

        // Bind the source PDF. After binding, the Document property gives access to pages.
        mend.BindPdf(inputPdf);

        // Determine number of pages (Aspose.Pdf uses 1‑based indexing).
        int pageCount = mend.Document.Pages.Count;

        // Fixed size for the signature image (in points). Adjust as needed.
        const float imgWidth  = 100f; // width of the image
        const float imgHeight = 100f; // height of the image

        // Add the image to the bottom‑left corner of every page.
        for (int pageNum = 1; pageNum <= pageCount; pageNum++)
        {
            // Open the PNG as a stream for each insertion.
            using (FileStream imgStream = File.OpenRead(signatureImage))
            {
                // lowerLeftX = 0, lowerLeftY = 0 places the image at the page origin.
                // upperRightX/Y define the image rectangle size.
                mend.AddImage(imgStream, pageNum, 0f, 0f, imgWidth, imgHeight);
            }
        }

        // Save the modified PDF to the output path.
        mend.Save(outputPdf);

        // Close the facade (releases internal resources).
        mend.Close();

        Console.WriteLine($"Signature image added to all pages. Output saved to '{outputPdf}'.");
    }
}