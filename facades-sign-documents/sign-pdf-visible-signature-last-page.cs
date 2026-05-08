using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and certificate details
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath  = "certificate.pfx";
        const string certPassword = "password";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
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
            // Get the last page (Aspose.Pdf uses 1‑based indexing)
            int lastPageNumber = doc.Pages.Count;
            Page lastPage = doc.Pages[lastPageNumber];

            // Define signature appearance size and margin (in points)
            const double sigWidth  = 150; // width of signature rectangle
            const double sigHeight = 50;  // height of signature rectangle
            const double margin    = 30;  // distance from page edges

            // Calculate rectangle coordinates (origin is lower‑left)
            double llx = lastPage.PageInfo.Width - sigWidth - margin; // lower‑left X
            double lly = margin;                                      // lower‑left Y

            // System.Drawing.Rectangle expects integer values
            var signatureRect = new System.Drawing.Rectangle(
                (int)Math.Round(llx),
                (int)Math.Round(lly),
                (int)Math.Round(sigWidth),
                (int)Math.Round(sigHeight));

            // Create and configure the PdfFileSignature facade
            using (PdfFileSignature pdfSigner = new PdfFileSignature())
            {
                // Bind the source PDF
                pdfSigner.BindPdf(inputPdf);

                // Optional: set a visual appearance image for the signature
                // pdfSigner.SignatureAppearance = "signature_image.jpg";

                // Provide the certificate for signing
                pdfSigner.SetCertificate(certPath, certPassword);

                // Apply a visible signature on the last page
                pdfSigner.Sign(
                    page:          lastPageNumber,
                    SigReason:     "Document approved",
                    SigContact:    "john.doe@example.com",
                    SigLocation:   "New York, USA",
                    visible:       true,
                    annotRect:     signatureRect);

                // Save the signed PDF
                pdfSigner.Save(outputPdf);
            }
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}