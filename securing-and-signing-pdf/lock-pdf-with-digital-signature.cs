using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the certificate and the output file
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_locked.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Create a PKCS#1 signature object using the certificate
        var signature = new PKCS1(certificatePath, certificatePassword)
        {
            Reason      = "Document approved",
            ContactInfo = "contact@example.com",
            Location    = "Head Office"
        };

        // Define the MDP (Modification Detection and Prevention) signature
        // NoChanges = 1 – any further modification invalidates the signature
        var mdpSignature = new Aspose.Pdf.Forms.DocMDPSignature(
            signature,
            Aspose.Pdf.Forms.DocMDPAccessPermissions.NoChanges);

        // Define the visible rectangle for the signature appearance (optional)
        // System.Drawing.Rectangle is required by the facade API
        var signatureRect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Use the PdfFileSignature facade via its fully qualified name
        var pdfSigner = new Aspose.Pdf.Facades.PdfFileSignature();

        // Bind the source PDF
        pdfSigner.BindPdf(inputPdfPath);

        // (Optional) Set an image to be used as the visual appearance of the signature
        // pdfSigner.SignatureAppearance = "signatureImage.png";

        // Apply the certification (MDP) signature on page 1
        pdfSigner.Certify(
            page: 1,
            SigReason: signature.Reason,
            SigContact: signature.ContactInfo,
            SigLocation: signature.Location,
            visible: true,
            annotRect: signatureRect,
            docMdpSignature: mdpSignature);

        // Save the signed and locked PDF
        pdfSigner.Save(outputPdfPath);
        pdfSigner.Close();

        Console.WriteLine($"Signed and read‑only PDF saved to '{outputPdfPath}'.");
    }
}