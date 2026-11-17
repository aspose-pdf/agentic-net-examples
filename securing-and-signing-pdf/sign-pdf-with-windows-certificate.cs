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
        const string thumbprint = "YOUR_CERTIFICATE_THUMBPRINT"; // replace with actual thumbprint (no spaces, case‑insensitive)

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the certificate from the current user's personal store
        X509Certificate2 certificate = FindCertificateByThumbprint(thumbprint);
        if (certificate == null)
        {
            Console.Error.WriteLine($"Certificate with thumbprint '{thumbprint}' not found in the store.");
            return;
        }

        // Open the PDF document
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // ------------------------------------------------------------
            // 1️⃣ Create a (invisible) signature field on the first page.
            // ------------------------------------------------------------
            Page page = pdfDocument.Pages[1];
            // An invisible rectangle – width/height = 0. Change if a visible appearance is required.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            var signatureField = new SignatureField(page, rect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the document's form collection.
            pdfDocument.Form.Add(signatureField, 1);

            // ------------------------------------------------------------
            // 2️⃣ Prepare the PKCS#7 signature object using the X509 cert.
            // ------------------------------------------------------------
            // Export the X509Certificate2 to a PFX (no password needed).
            byte[] pfxBytes = certificate.Export(X509ContentType.Pfx);
            using var pfxStream = new MemoryStream(pfxBytes);
            var pkcs7 = new PKCS7(pfxStream, string.Empty)
            {
                Reason = "Document approval",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // ------------------------------------------------------------
            // 3️⃣ Sign the field.
            // ------------------------------------------------------------
            signatureField.Sign(pkcs7);

            // Save the signed PDF.
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }

    // Helper method to locate a certificate by thumbprint in the CurrentUser\My store
    private static X509Certificate2 FindCertificateByThumbprint(string thumbprint)
    {
        using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly);
        X509Certificate2Collection found = store.Certificates.Find(
            X509FindType.FindByThumbprint,
            thumbprint,
            validOnly: false);
        return found.Count > 0 ? found[0] : null;
    }
}