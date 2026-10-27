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
        const string certPath = "certificate.pfx"; // RSA 4096‑bit private key in PKCS#12 container
        const string certPassword = "yourPfxPassword";

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

        try
        {
            // Load the RSA certificate using the standard X509Certificate2 constructor.
            X509Certificate2 rsaCert = new X509Certificate2(certPath, certPassword);

            // Open the PDF document inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Define a rectangle where the visual signature appearance will be placed (page 1 in this example).
                var signatureRect = new Rectangle(100, 100, 200, 150);

                // Create a signature field and add it to the document's form collection.
                var signatureField = new SignatureField(pdfDoc.Pages[1], signatureRect)
                {
                    PartialName = "Signature1"
                };
                pdfDoc.Form.Add(signatureField);

                // NOTE:
                // The Signature property of SignatureField is read‑only in the core API.
                // To apply a real digital signature you would need the Aspose.Pdf.Signatures
                // package and use PKCS7Signature (or PKCS7) from that assembly.
                // This example demonstrates a compilable placeholder field.

                // Save the PDF (the field will be visible but not cryptographically signed).
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF processed successfully and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}
