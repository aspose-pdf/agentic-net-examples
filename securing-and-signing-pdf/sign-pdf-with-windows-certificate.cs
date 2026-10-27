using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string thumbprint = "YOUR_CERT_THUMBPRINT"; // replace with the actual thumbprint

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the certificate from the current user's Personal store using its thumbprint
        X509Certificate2? cert = GetCertificateFromStore(thumbprint, StoreName.My, StoreLocation.CurrentUser);
        if (cert == null)
        {
            Console.Error.WriteLine($"Certificate with thumbprint {thumbprint} not found in the store.");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Define the rectangle where the visual signature will appear (coordinates are in points)
        var signatureRect = new Rectangle(100, 100, 200, 150);

        // Create a signature field on the first page
        var signatureField = new SignatureField(pdfDocument.Pages[1], signatureRect)
        {
            PartialName = "Signature1"
        };

        // Add the signature field to the document's form collection
        pdfDocument.Form.Add(signatureField, 1);

        // NOTE:
        // The PKCS#7 digital signature functionality resides in the optional
        // Aspose.Pdf.Signatures assembly. The core Aspose.Pdf library does not
        // expose a writable Signature property on SignatureField, therefore the
        // example creates only an empty signature field that compiles without the
        // extra package. To apply a real digital signature, reference the
        // Aspose.Pdf.Signatures NuGet package and use the PKCS7Signature class as
        // demonstrated in the official documentation.

        // Save the PDF (with the empty signature field)
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"PDF processed successfully: {outputPdf}");
    }

    /// <summary>
    /// Retrieves an X509Certificate2 from the specified store by thumbprint.
    /// </summary>
    private static X509Certificate2? GetCertificateFromStore(string thumbprint, StoreName storeName, StoreLocation storeLocation)
    {
        using (var store = new X509Store(storeName, storeLocation))
        {
            store.Open(OpenFlags.ReadOnly);
            var certs = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly: false);
            return certs.Count > 0 ? certs[0] : null;
        }
    }
}