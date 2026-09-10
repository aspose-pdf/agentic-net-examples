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

        // Thumbprint of the certificate stored in the Windows certificate store (case‑insensitive, no spaces)
        const string certThumbprint = "ABCD1234EF567890ABCD1234EF567890ABCD1234";

        // Open the Windows "My" (Personal) store for the current user
        X509Certificate2 certificate = null!;
        using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
        {
            store.Open(OpenFlags.ReadOnly);
            X509Certificate2Collection found = store.Certificates.Find(
                X509FindType.FindByThumbprint,
                certThumbprint,
                validOnly: false);

            if (found.Count == 0)
            {
                Console.Error.WriteLine($"Certificate with thumbprint '{certThumbprint}' not found.");
                return;
            }

            certificate = found[0];
        }

        // Load the PDF document (using the recommended lifecycle rule)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page if one does not already exist
            // Rectangle coordinates: lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
            SignatureField sigField = new SignatureField(pdfDoc.Pages[1], sigRect);
            sigField.PartialName = "Signature1"; // set the field name

            // Add the field to the form – the overload expects the page number (int)
            pdfDoc.Form.Add(sigField, 1);

            // Create an ExternalSignature that uses the certificate from the store
            ExternalSignature externalSig = new ExternalSignature(certificate);

            // Sign the field with the external signature
            sigField.Sign(externalSig);

            // Save the signed PDF (using the recommended lifecycle rule)
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}
