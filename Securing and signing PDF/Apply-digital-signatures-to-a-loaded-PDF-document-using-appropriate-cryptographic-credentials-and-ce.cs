using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades; // needed for ExternalSignature

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string signatureFieldName = "Signature1";

        // Ensure the source PDF exists – create a minimal one if it does not.
        if (!File.Exists(inputPdfPath))
        {
            using (var tempDoc = new Document())
            {
                tempDoc.Pages.Add(); // add a blank page
                tempDoc.Save(inputPdfPath);
            }
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Ensure the document has at least one page – required for creating a signature field
            if (doc.Pages.Count == 0)
            {
                doc.Pages.Add();
            }

            // Rectangle for the signature appearance (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field and add it to the form collection
            // SignatureField constructor requires the target page and the rectangle
            Page firstPage = doc.Pages[1]; // Pages collection is 1‑based
            SignatureField sigField = new SignatureField(firstPage, sigRect)
            {
                PartialName = signatureFieldName
            };
            doc.Form.Add(sigField);

            // Retrieve a signing certificate from the current user's personal store
            X509Certificate2 signingCert = GetCertificateFromStore(
                StoreName.My,
                StoreLocation.CurrentUser,
                "CN=MySigningCertificate"); // adjust the subject name as needed

            if (signingCert == null)
            {
                Console.Error.WriteLine("Signing certificate not found.");
                return;
            }

            // Create an Aspose.Pdf signature object using the certificate
            // ExternalSignature creates a detached PKCS#7 signature suitable for PDF signing
            ExternalSignature signature = new ExternalSignature(signingCert)
            {
                Reason = "Document approved",
                Location = Environment.MachineName,
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the signature field
            sigField.Sign(signature);

            // Save the signed PDF (lifecycle rule: save within the using block)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }

    // Helper method to locate a certificate in a store by subject name (case‑insensitive)
    private static X509Certificate2 GetCertificateFromStore(StoreName storeName, StoreLocation storeLocation, string subjectContains)
    {
        using (X509Store store = new X509Store(storeName, storeLocation))
        {
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection certs = store.Certificates
                .Find(X509FindType.FindBySubjectName, subjectContains, validOnly: false);
            return certs.Cast<X509Certificate2>().FirstOrDefault();
        }
    }
}
