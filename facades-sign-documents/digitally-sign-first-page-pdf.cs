using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF, output PDF, and the signing certificate
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        // Verify that required files exist
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

        // Create the PdfFileSignature facade
        PdfFileSignature pdfSigner = new PdfFileSignature();

        // Bind the PDF to be signed
        pdfSigner.BindPdf(inputPdf);

        // Optional: set a visual appearance for the signature (e.g., an image)
        // pdfSigner.SignatureAppearance = "signature_image.jpg";

        // Provide the certificate and its password
        pdfSigner.SetCertificate(certificatePath, certificatePassword);

        // Define the rectangle (position and size) for the visible signature on page 1
        // System.Drawing.Rectangle is required by the Sign method overload
        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Apply the digital signature to page 1
        pdfSigner.Sign(
            page: 1,
            SigReason: "Document approved",
            SigContact: "john.doe@example.com",
            SigLocation: "New York",
            visible: true,
            annotRect: signatureRect);

        // Save the signed PDF
        pdfSigner.Save(outputPdf);

        // Close the facade (good practice, though not strictly required)
        pdfSigner.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}