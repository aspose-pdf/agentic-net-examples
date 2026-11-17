using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // PKCS7, PKCS7Detached, SignatureField, etc.

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF from a file stream (deterministic disposal)
        using (FileStream pdfStream = File.OpenRead(inputPdfPath))
        {
            Document pdfDoc = new Document(pdfStream);

            // Create an invisible signature field – a zero‑size rectangle makes the field non‑visible
            var rect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            SignatureField sigField = new SignatureField(pdfDoc, rect)
            {
                PartialName = "DetachedSignature"
            };

            // Add the field to the first page (pages are 1‑based)
            pdfDoc.Form.Add(sigField, 1);

            // Prepare a PKCS#7 **detached** signature. Use PKCS7Detached instead of PKCS7 and do not set SignatureType.
            PKCS7Detached pkcs7 = new PKCS7Detached(certificatePath, certificatePassword)
            {
                Reason = "Document signed",
                Location = "My Company",
                ContactInfo = "contact@mycompany.com"
                // No SignatureType property – the class itself represents a detached signature.
            };

            // Sign the field (the field is invisible because its rectangle is zero‑size)
            sigField.Sign(pkcs7);

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}
