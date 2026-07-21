using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, the signed output PDF and the certificate (PFX) file.
        const string inputPdf = "input.pdf";
        const string signedPdf = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        // Verify that required files exist.
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // -------------------------------------------------
        // Sign the PDF using PdfFileSignature (facade API)
        // -------------------------------------------------
        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            // Bind the source PDF file.
            pdfSigner.BindPdf(inputPdf);

            // Set the certificate (PFX) and its password.
            pdfSigner.SetCertificate(certificatePath, certificatePassword);

            // Optional: set a visual appearance for the signature.
            // pdfSigner.SignatureAppearance = "signature_appearance.png";

            // Define the rectangle where the visible signature will be placed.
            // PdfFileSignature expects System.Drawing.Rectangle.
            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign the first page. Parameters: page number (1‑based), reason, contact, location,
            // visibility flag, and the rectangle.
            pdfSigner.Sign(
                page: 1,
                SigReason: "Document approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York",
                visible: true,
                annotRect: signatureRect);

            // Save the signed PDF.
            pdfSigner.Save(signedPdf);
        }

        // -------------------------------------------------
        // Verify the signature(s) in the signed PDF
        // -------------------------------------------------
        using (PdfFileSignature pdfVerifier = new PdfFileSignature())
        {
            // Bind the signed PDF file.
            pdfVerifier.BindPdf(signedPdf);

            // Retrieve all signature names (true => include empty fields, false => only filled).
            var signatureNames = pdfVerifier.GetSignatureNames(true);

            // Iterate over each signature and verify its authenticity using the new API.
            foreach (var sigName in signatureNames)
            {
                // VerifySignature returns a boolean indicating validity.
                bool isValid = pdfVerifier.VerifySignature(sigName);
                Console.WriteLine($"Signature '{sigName.Name}' validity: {isValid}");
            }

            // Additionally, you can check if the document contains any signatures at all.
            bool hasSignature = pdfVerifier.ContainsSignature();
            Console.WriteLine($"Document contains signature(s): {hasSignature}");
        }
    }
}
