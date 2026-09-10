using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and certificate (PFX) paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_final.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Disallow further signatures by enabling append‑only mode.
            // This forces any later modifications to be saved as incremental updates,
            // which would invalidate additional signatures.
            doc.Form.SignaturesAppendOnly = true;

            // Create a signature field on the first page.
            // Rectangle coordinates are (llx, lly, urx, ury) in points.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(sigField, 1); // Add the field to the document (page index is 1‑based)

            // Prepare the PKCS#1 signature object using the certificate.
            PKCS1 pkcs1 = new PKCS1(certificatePath, certificatePassword)
            {
                Reason = "Document approved",
                ContactInfo = "contact@example.com",
                Location = "Head Office"
            };

            // Sign the field.
            sigField.Sign(pkcs1);

            // Save the signed PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdf}'. No further signatures can be added without invalidating the document.");
    }
}