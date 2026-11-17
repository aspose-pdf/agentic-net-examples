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
        const string outputPdf = "signed_output.pdf";
        const string signatureFieldName = "Signature1";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Locate a certificate that has a private key stored on a smart card.
        X509Certificate2 signingCert = FindSigningCertificate();
        if (signingCert == null)
        {
            Console.Error.WriteLine("No suitable signing certificate found in the CurrentUser store.");
            return;
        }

        // Prompt the user for the smart‑card PIN. The middleware will display the PIN dialog
        // when the private key is accessed, so we only pause here.
        Console.Write("Enter smart card PIN (if prompted by middleware): ");
        Console.ReadLine();

        // Load the PDF into an expandable memory stream – signing writes directly into this stream.
        byte[] pdfBytes = File.ReadAllBytes(inputPdf);
        using var inputStream = new MemoryStream();
        inputStream.Write(pdfBytes, 0, pdfBytes.Length);
        inputStream.Position = 0;

        using var doc = new Document(inputStream);

        // Add a signature field (visible appearance is optional).
        var rect = new Rectangle(100, 100, 300, 150); // lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
        var sigField = new SignatureField(doc.Pages[1], rect) { PartialName = signatureFieldName };
        doc.Form.Add(sigField, 1);

        // Create an external signature that uses the smart‑card certificate.
        var externalSig = new ExternalSignature(signingCert)
        {
            Reason = "Document approved",
            Location = "Office",
            ContactInfo = "user@example.com",
            // Optional visual customisation.
            CustomAppearance = new SignatureCustomAppearance { FontSize = 8, ShowContactInfo = false }
        };

        // Sign the field. The signature is written into the source stream; do NOT call doc.Save().
        sigField.Sign(externalSig);

        // Write the signed PDF to the output file.
        File.WriteAllBytes(outputPdf, inputStream.ToArray());
        Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdf}'.");
    }

    static X509Certificate2 FindSigningCertificate()
    {
        using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly);
        foreach (var cert in store.Certificates)
        {
            if (cert.HasPrivateKey && cert.NotAfter > DateTime.Now && cert.NotBefore < DateTime.Now)
            {
                // Additional filtering (subject, thumbprint, etc.) can be added here.
                return cert;
            }
        }
        return null;
    }
}
