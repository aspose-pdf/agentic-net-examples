using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";      // PDF to be stamped
        const string stampPdf   = "stamp.pdf";      // PDF containing the stamp page
        const string outputPdf  = "output.pdf";     // Resulting PDF

        if (!File.Exists(inputPdf) || !File.Exists(stampPdf))
        {
            Console.Error.WriteLine("Input or stamp file not found.");
            return;
        }

        // Load the target document to obtain page dimensions (page 8 is 1‑based)
        double pageWidth, pageHeight;
        using (Document doc = new Document(inputPdf))
        {
            if (doc.Pages.Count < 8)
            {
                Console.Error.WriteLine("The document has fewer than 8 pages.");
                return;
            }

            pageWidth  = doc.Pages[8].PageInfo.Width;
            pageHeight = doc.Pages[8].PageInfo.Height;
        }

        // Load the stamp document to obtain its size (first page is used as the stamp)
        double stampWidth, stampHeight;
        using (Document stampDoc = new Document(stampPdf))
        {
            stampWidth  = stampDoc.Pages[1].PageInfo.Width;
            stampHeight = stampDoc.Pages[1].PageInfo.Height;
        }

        // Compute the origin so that the stamp is centred on page 8.
        // Origin is the lower‑left corner of the stamp.
        float originX = (float)((pageWidth  - stampWidth)  / 2.0);
        float originY = (float)((pageHeight - stampHeight) / 2.0);

        // Create the facade that will apply the stamp.
        using (PdfFileStamp fileStamp = new PdfFileStamp())
        {
            // Bind the PDF that will receive the stamp.
            fileStamp.BindPdf(inputPdf);

            // Create a stamp that uses the first page of the stamp PDF.
            Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
            stamp.BindPdf(stampPdf, 1);          // use page 1 of stamp.pdf as the stamp content
            stamp.IsBackground = false;          // stamp appears on top of page content
            stamp.SetOrigin(originX, originY);   // position the stamp (centred)
            stamp.Pages = new int[] { 8 };       // apply only to page 8

            // Add the stamp to the document.
            fileStamp.AddStamp(stamp);

            // Save the result.
            fileStamp.Save(outputPdf);
            fileStamp.Close();
        }

        Console.WriteLine($"Stamp repositioned and saved to '{outputPdf}'.");
    }
}
