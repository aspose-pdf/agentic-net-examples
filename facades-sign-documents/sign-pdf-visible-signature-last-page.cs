using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and certificate details
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        // Optional image to be used as the visual appearance of the signature
        const string signatureImage = "signature_appearance.jpg";

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

        // Use the PdfFileSignature facade to sign the document
        using (var pdfSign = new Aspose.Pdf.Facades.PdfFileSignature())
        {
            // Bind the source PDF
            pdfSign.BindPdf(inputPdf);

            // Determine the last page number (Aspose.Pdf uses 1‑based indexing)
            int lastPageNumber = pdfSign.Document.Pages.Count;

            // Retrieve page dimensions
            Page lastPage = pdfSign.Document.Pages[lastPageNumber];
            double pageWidth  = lastPage.PageInfo.Width;
            double pageHeight = lastPage.PageInfo.Height;

            // Define signature rectangle (bottom‑right corner)
            // Width and height of the visible signature field (in points)
            const int sigWidth  = 150;
            const int sigHeight = 50;
            // Margin from the right and bottom edges
            const int margin = 10;

            int rectX = (int)(pageWidth  - sigWidth - margin);
            int rectY = margin; // distance from bottom edge

            // Fully qualified System.Drawing.Rectangle to avoid ambiguity
            var signatureRect = new System.Drawing.Rectangle(rectX, rectY, sigWidth, sigHeight);

            // Optional: set a graphic appearance for the signature
            pdfSign.SignatureAppearance = signatureImage;

            // Provide the certificate used for signing
            pdfSign.SetCertificate(certPath, certPass);

            // Sign the last page with a visible signature
            pdfSign.Sign(
                page:          lastPageNumber,
                SigReason:     "Document approved",
                SigContact:    "contact@example.com",
                SigLocation:   "Head Office",
                visible:       true,
                annotRect:     signatureRect);

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}