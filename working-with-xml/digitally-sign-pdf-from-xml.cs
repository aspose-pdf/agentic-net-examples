using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing; // needed for System.Drawing.Rectangle

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath = "input.xml";
        const string signedPdfPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const string signatureImagePath = "signature.png"; // optional visual appearance

        // Validate required files
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Create a PDF document from the XML source
        // -----------------------------------------------------------------
        using (Document pdfDoc = new Document())
        {
            // Bind the XML (and optional XSLT) to the document
            pdfDoc.BindXml(xmlPath);

            // -----------------------------------------------------------------
            // 2. Digitally sign the generated PDF using PdfFileSignature
            // -----------------------------------------------------------------
            // Initialize the signing facade on the in‑memory document
            PdfFileSignature signer = new PdfFileSignature(pdfDoc);

            // Set the certificate (PKCS#12) and its password
            signer.SetCertificate(certificatePath, certificatePassword);

            // Optional: set a visible signature appearance (image)
            signer.SignatureAppearance = signatureImagePath;

            // Define the signature rectangle (position and size) on page 1
            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Apply the signature
            signer.Sign(
                page: 1,                     // 1‑based page index
                SigReason: "Document signed", // reason for signing
                SigContact: "Signer Contact", // contact information
                SigLocation: "Signer Location", // location information
                visible: true,               // make the signature visible
                annotRect: signatureRect);   // rectangle where the signature appears

            // Save the signed PDF to the specified file
            signer.Save(signedPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{signedPdfPath}'.");
    }
}