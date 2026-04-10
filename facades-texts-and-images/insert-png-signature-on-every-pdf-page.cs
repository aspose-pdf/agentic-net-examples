using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf       = "input.pdf";        // source PDF
        const string outputPdf      = "signed_output.pdf";// result PDF
        const string signatureImage = "signature.png";    // PNG to place on each page

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(signatureImage))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImage}");
            return;
        }

        // Determine all page numbers (Aspose.Pdf uses 1‑based indexing)
        int[] allPages;
        using (Document doc = new Document(inputPdf))
        {
            int pageCount = doc.Pages.Count;
            allPages = Enumerable.Range(1, pageCount).ToArray();
        }

        // Use PdfFileMend to add the PNG to every page
        using (PdfFileMend mend = new PdfFileMend())
        {
            // Load the PDF into the facade
            mend.BindPdf(inputPdf);

            // Add the image to the bottom‑left corner of each page.
            // Coordinates: lower‑left (0,0), upper‑right (100,100) → 100×100 pt image.
            bool added = mend.AddImage(signatureImage, allPages, 0f, 0f, 100f, 100f);
            if (!added)
            {
                Console.Error.WriteLine("Failed to add the signature image to the PDF.");
                return;
            }

            // Persist the changes
            mend.Save(outputPdf);
        }

        Console.WriteLine($"Signature image added to all pages. Output saved to '{outputPdf}'.");
    }
}