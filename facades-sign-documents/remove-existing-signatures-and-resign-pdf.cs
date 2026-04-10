using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";          // PDF with existing signature(s)
        const string outputPdfPath  = "output_signed.pdf"; // Resulting PDF after re‑signing
        const string certPath       = "newcert.pfx";        // Updated certificate file (PKCS#12)
        const string certPassword   = "certPassword";       // Password for the certificate

        // Verify that the input PDF exists
        if (!System.IO.File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Initialise the facade and bind the source PDF
            using (PdfFileSignature pdfSigner = new PdfFileSignature())
            {
                pdfSigner.BindPdf(inputPdfPath);

                // 1) Remove all existing signatures
                pdfSigner.RemoveSignatures();

                // 2) Configure the new certificate
                pdfSigner.SetCertificate(certPath, certPassword);

                // Optional: set a visible appearance (image file) for the signature
                // pdfSigner.SignatureAppearance = "signature_appearance.jpg";

                // 3) Define the rectangle where the signature will be placed
                // (x, y, width, height) – coordinates are in points (1/72 inch)
                Rectangle signatureRect = new Rectangle(100, 100, 200, 50);

                // 4) Apply the new signature on page 1 (pages are 1‑based)
                pdfSigner.Sign(
                    page:          1,
                    SigReason:     "Document updated",
                    SigContact:    "admin@example.com",
                    SigLocation:   "Head Office",
                    visible:       true,
                    annotRect:     signatureRect);

                // 5) Save the signed PDF
                pdfSigner.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF re‑signed successfully: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}