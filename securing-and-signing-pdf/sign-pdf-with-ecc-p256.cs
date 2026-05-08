using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;          // SignatureField, ExternalSignature
using Aspose.Pdf.Annotations;   // Rectangle (fully qualified to avoid ambiguity)

class SignPdfWithEcc
{
    static void Main()
    {
        // Input PDF, output PDF, and ECC P‑256 certificate (PFX) with password
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string pfxPath       = "ecc_certificate.pfx";
        const string pfxPassword   = "pfxPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page
            Page page = doc.Pages[1];

            // Define the rectangle where the signature appearance will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

            // Create a signature field and add it to the form
            SignatureField signatureField = new SignatureField(page, rect);
            doc.Form.Add(signatureField);

            // Load the ECC certificate (must contain a private key on curve P‑256)
            X509Certificate2 cert = new X509Certificate2(pfxPath, pfxPassword, X509KeyStorageFlags.Exportable);

            // Create an ExternalSignature that will use the certificate's private key
            ExternalSignature externalSignature = new ExternalSignature(cert);

            // Optional: set signature properties (reason, location, etc.)
            externalSignature.Reason   = "Document approved";
            externalSignature.Location = "New York, USA";
            externalSignature.ContactInfo = "contact@example.com";

            // Sign the PDF using the ECC key – Aspose.Pdf will select the appropriate ECDSA algorithm
            signatureField.Sign(externalSignature);

            // Save the signed PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}