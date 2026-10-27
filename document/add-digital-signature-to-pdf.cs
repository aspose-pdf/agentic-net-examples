using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "pfxPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Load the PDF and add a visible signature field on the first page
        // -----------------------------------------------------------------
        Document pdfDoc = new Document(inputPdfPath);
        // Define the rectangle where the signature will appear (llx, lly, urx, ury)
        Rectangle sigRect = new Rectangle(100, 100, 300, 200);

        // Create the signature field and assign a unique name
        SignatureField sigField = new SignatureField(pdfDoc, sigRect)
        {
            Name = "Signature1"
        };

        // Add the field to the document's form collection
        pdfDoc.Form.Add(sigField);

        // -----------------------------------------------------------------
        // 2. Load the self‑signed certificate (PFX) that will be used for signing
        // -----------------------------------------------------------------
        // Use the non‑obsolete constructor that loads the certificate directly
        X509Certificate2 signingCert = new X509Certificate2(certPath, certPassword, X509KeyStorageFlags.DefaultKeySet);

        // -----------------------------------------------------------------
        // 3. Prepare PKCS#7 signature object (Aspose.Pdf.Forms) and optional appearance
        // -----------------------------------------------------------------
        PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
        // Optional custom visual appearance
        pkcs7.CustomAppearance = new SignatureCustomAppearance
        {
            ShowContactInfo = true,
            ShowLocation = true,
            ContactInfoLabel = "Contact:",
            LocationLabel = "Location:"
        };

        // -----------------------------------------------------------------
        // 4. Sign the PDF using the field added in step 1
        // -----------------------------------------------------------------
        // The Sign overload for SignatureField takes only the PKCS7 object.
        sigField.Sign(pkcs7);

        // Save the signed PDF
        pdfDoc.Save(outputPdfPath);

        Console.WriteLine($"PDF signed successfully: {outputPdfPath}");
    }
}