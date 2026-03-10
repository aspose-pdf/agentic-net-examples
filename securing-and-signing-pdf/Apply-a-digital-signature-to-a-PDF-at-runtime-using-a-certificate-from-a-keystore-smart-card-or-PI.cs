using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the signed output PDF
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";

        // -----------------------------------------------------------------
        // Example 1: Sign using a PFX file (certificate with private key)
        // -----------------------------------------------------------------
        const string pfxPath     = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        // -----------------------------------------------------------------
        // Example 2: Sign using a certificate from a smart card / PIV device
        // -----------------------------------------------------------------
        // Uncomment the following block if you prefer to use a certificate
        // stored in a Windows certificate store (e.g., smart card).
        /*
        X509Certificate2 smartCardCert = null;
        using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            // Select the appropriate certificate – here we just take the first one.
            // In real code you should locate the certificate by thumbprint or subject.
            if (store.Certificates.Count > 0)
                smartCardCert = store.Certificates[0];
        }
        if (smartCardCert == null)
        {
            Console.Error.WriteLine("No certificate found in the store.");
            return;
        }
        */

        // -----------------------------------------------------------------
        // Load the PDF document (lifecycle rule: use provided load pattern)
        // -----------------------------------------------------------------
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear.
            // Fully qualified type to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field and add it to the document's form.
            SignatureField sigField = new SignatureField(doc, sigRect);
            doc.Form.Add(sigField);

            // -----------------------------------------------------------------
            // Choose the signing method.
            // -----------------------------------------------------------------
            // 1) Using a PFX file (PKCS#7 signature)
            Signature pkcs7Signature = new PKCS7(pfxPath, pfxPassword);
            sigField.Sign(pkcs7Signature);

            // 2) Using a certificate from a smart card / PIV device (ExternalSignature)
            // Uncomment the following lines if you are using a smart‑card certificate.
            /*
            Signature externalSignature = new ExternalSignature(smartCardCert);
            sigField.Sign(externalSignature);
            */

            // -----------------------------------------------------------------
            // Save the signed PDF (lifecycle rule: use provided save pattern)
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}