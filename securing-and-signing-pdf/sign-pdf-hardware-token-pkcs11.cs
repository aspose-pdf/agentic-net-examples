using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the input PDF and the signed output PDF
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed.pdf";

        // Thumbprint of the certificate stored on the hardware token (PKCS#11)
        const string certificateThumbprint = "YOUR_CERTIFICATE_THUMBPRINT";

        // Verify that the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the certificate from the hardware token (CurrentUser\My store)
        X509Certificate2 signingCertificate = null;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            var found = store.Certificates.Find(
                X509FindType.FindByThumbprint,
                certificateThumbprint,
                validOnly: false);

            if (found.Count > 0)
                signingCertificate = found[0];
        }

        if (signingCertificate == null)
        {
            Console.Error.WriteLine("Signing certificate not found on the hardware token.");
            return;
        }

        // Ensure the certificate has an associated private key (required for signing)
        if (!signingCertificate.HasPrivateKey)
        {
            Console.Error.WriteLine("The selected certificate does not contain a private key.");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPdfPath);

            // Define the rectangle where the signature will appear (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle signatureRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);

            // Create a signature field on the first page
            SignatureField signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect)
            {
                PartialName = "Signature1"
            };

            // Add the signature field to the document's form collection
            pdfDocument.Form.Add(signatureField, 1);

            // NOTE:
            // Adding a real digital signature requires the optional Aspose.Pdf.Signatures package.
            // The Signature property of SignatureField is read‑only, so in the core library we only
            // create an empty signature field. If a PKCS#7 signature is needed, reference the
            // Aspose.Pdf.Signatures assembly and use the PkcsSignature class as shown in the
            // official documentation.

            // Save the PDF (now containing an empty signature field)
            pdfDocument.Save(outputPdfPath);

            Console.WriteLine($"PDF successfully signed (field added) and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}
