using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_output.pdf"; // signed PDF
        const string certPath   = "certificate.pfx";   // signing certificate
        const string certPass   = "password";          // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the document to obtain the size of the last page
        using (Document doc = new Document(inputPdf))
        {
            int lastPageNumber = doc.Pages.Count; // 1‑based indexing
            Page lastPage = doc.Pages[lastPageNumber];

            // Determine a rectangle positioned at the bottom‑right corner
            // Adjust width/height as needed (here 150x50 points)
            int rectWidth  = 150;
            int rectHeight = 50;
            int rectX = (int)(lastPage.PageInfo.Width  - rectWidth  - 20); // 20‑point margin from right edge
            int rectY = (int)(lastPage.PageInfo.Height - rectHeight - 20); // 20‑point margin from bottom edge

            // PdfFileSignature expects a System.Drawing.Rectangle, not Aspose.Pdf.Rectangle
            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(rectX, rectY, rectWidth, rectHeight);

            // Create and configure the PdfFileSignature facade
            using (PdfFileSignature pdfSign = new PdfFileSignature())
            {
                pdfSign.BindPdf(inputPdf);                         // load PDF
                pdfSign.SetCertificate(certPath, certPass);        // set signing certificate

                // Optional: set a graphic appearance for the signature (e.g., a logo)
                // pdfSign.SignatureAppearance = "signature_logo.jpg";

                // Apply a visible signature on the last page
                pdfSign.Sign(
                    page:          lastPageNumber,
                    SigReason:     "Approved",
                    SigContact:    "john.doe@example.com",
                    SigLocation:   "New York",
                    visible:       true,
                    annotRect:     signatureRect);

                // Save the signed PDF
                pdfSign.Save(outputPdf);
            }
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
