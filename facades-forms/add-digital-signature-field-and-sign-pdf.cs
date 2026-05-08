using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // for FieldType, PKCS1

class Program
{
    static void Main()
    {
        // Input and output files
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certFile   = "certificate.pfx";
        const string certPass   = "password";
        const string appearance = "signature_appearance.png";

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certFile))
        {
            Console.Error.WriteLine($"Certificate file not found: {certFile}");
            return;
        }
        if (!File.Exists(appearance))
        {
            Console.Error.WriteLine($"Signature appearance image not found: {appearance}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Add a signature field named "DigitalSignature" on page 1
            // Rectangle coordinates: lower‑left (100,100), upper‑right (200,150)
            using (FormEditor formEditor = new FormEditor(doc))
            {
                bool fieldAdded = formEditor.AddField(
                    FieldType.Signature,          // field type
                    "DigitalSignature",          // field name
                    1,                           // page number (1‑based)
                    100f, 100f,                  // llx, lly
                    200f, 150f);                 // urx, ury

                if (!fieldAdded)
                {
                    Console.Error.WriteLine("Failed to add signature field.");
                    return;
                }

                // Persist the added field into the document
                formEditor.Save();
            }

            // Prepare the signature object (PKCS1)
            PKCS1 pkcs1Signature = new PKCS1(certFile, certPass)
            {
                Reason      = "Document approved",
                ContactInfo = "contact@example.com",
                Location    = "Office"
            };

            // Sign the document using the newly created field
            using (PdfFileSignature pdfSigner = new PdfFileSignature())
            {
                // Bind the in‑memory document to the signer
                pdfSigner.BindPdf(doc);

                // Set visual appearance (image) for the signature
                pdfSigner.SignatureAppearance = appearance;

                // Set the certificate (required for signing)
                pdfSigner.SetCertificate(certFile, certPass);

                // Sign the field by its name
                pdfSigner.Sign("DigitalSignature", pkcs1Signature);

                // Save the signed PDF
                pdfSigner.Save(outputPdf);
            }

            Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
        }
    }
}