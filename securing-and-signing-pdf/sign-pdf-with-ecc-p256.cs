using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Security; // for ECDSA‑related types if needed

class SignPdfWithEcc
{
    static void Main()
    {
        // Paths – adjust as necessary
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string pfxPath       = "ecc_certificate.pfx";
        const string pfxPassword   = "yourPfxPassword";

        // Ensure source files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page.
            // Fully‑qualified Rectangle avoids ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(pdfDoc.Pages[1], sigRect)
            {
                // The name used to reference the field later (optional but recommended)
                PartialName = "Signature1"
            };

            // Add the signature field to the document's form collection.
            // AppendChild is not used here; Form.Add is the correct API for form fields.
            pdfDoc.Form.Add(sigField, 1);

            // Load the ECC certificate (P‑256) from the PFX file.
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                // PKCS7 works with any certificate that contains a private key,
                // including ECC keys. The digest algorithm is chosen automatically.
                PKCS7 pkcs7Signature = new PKCS7(pfxStream, pfxPassword);

                // Optional: set appearance or metadata (e.g., reason, location).
                pkcs7Signature.Reason   = "Document approved";
                pkcs7Signature.Location = "New York, USA";

                // Sign the field. The Sign method takes the Signature object.
                sigField.Sign(pkcs7Signature);
            }

            // Save the signed PDF (lifecycle rule: save inside using block).
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}