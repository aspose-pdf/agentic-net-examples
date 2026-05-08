using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "signed_output.pdf";  // signed PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Create a signature field on the first page.
            // -----------------------------------------------------------------
            // Rectangle coordinates are fully qualified to avoid ambiguity.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect)
            {
                PartialName = "Signature1",          // field identifier
                // Optional visual properties can be set here, e.g.:
                // Background = true,
                // Color = Aspose.Pdf.Color.LightGray
            };

            // Add the signature field to the document's form.
            doc.Form.Add(sigField);

            // -----------------------------------------------------------------
            // 2. Obtain the X509Certificate2 from the smart card.
            // -----------------------------------------------------------------
            // The certificate is retrieved from the personal store (My) of the
            // current user. Adjust StoreLocation/StoreName as needed for your
            // environment. The smart‑card provider supplies the private key.
            X509Certificate2 cert = null;
            using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadOnly);
                // Choose the appropriate certificate – here we pick the first one.
                // In a real scenario you would filter by subject, thumbprint, etc.
                if (store.Certificates.Count > 0)
                {
                    cert = store.Certificates[0];
                }
                store.Close();
            }

            if (cert == null)
            {
                Console.Error.WriteLine("No certificate found in the smart‑card store.");
                return;
            }

            // -----------------------------------------------------------------
            // 3. Create an ExternalSignature that uses the smart‑card certificate.
            // -----------------------------------------------------------------
            // ExternalSignature works with certificates whose private keys are
            // non‑exportable (e.g., on a smart card). It performs a detached
            // PKCS#7 signature without prompting for a PIN if the underlying
            // CSP/KSP is configured for silent signing.
            ExternalSignature externalSig = new ExternalSignature(cert)
            {
                Reason   = "Document approved",
                Location = "Office",
                Date     = DateTime.UtcNow,
                // Authority, ContactInfo, etc., can be set as needed.
            };

            // -----------------------------------------------------------------
            // 4. Sign the PDF using the signature field.
            // -----------------------------------------------------------------
            // The Sign method takes the Signature object (ExternalSignature in this case).
            sigField.Sign(externalSig);

            // -----------------------------------------------------------------
            // 5. Save the signed PDF (lifecycle rule: use using, then Save).
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed successfully: {outputPdf}");
    }
}