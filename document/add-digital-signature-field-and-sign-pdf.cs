using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;   // SignatureField, PKCS1, Signature

class Program
{
    static void Main()
    {
        // Input PDF, output signed PDF, and self‑signed certificate (PFX) details
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "selfsigned.pfx";
        const string certificatePassword = "pfxPassword";

        // Verify that required files exist
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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field and add it to the document
            // The constructor with Document automatically registers the field
            SignatureField signatureField = new SignatureField(doc, sigRect)
            {
                // Optional: set a name for the field (used as the form field identifier)
                Name = "Signature1",
                // Optional: tooltip shown in PDF viewers
                AlternateName = "Document Signature"
            };

            // Create a PKCS#1 signature object using the self‑signed certificate
            // This constructor loads the certificate from the PFX file and the password
            PKCS1 pkcs1Signature = new PKCS1(certificatePath, certificatePassword)
            {
                // Optional metadata for the signature appearance
                Reason   = "I approve the contents of this document.",
                Location = "My Office",
                ContactInfo = "email@example.com",
                Date = DateTime.Now
            };

            // Sign the document using the created signature field
            // The overload without stream/password uses the certificate already loaded in the PKCS1 object
            signatureField.Sign(pkcs1Signature);

            // Save the signed PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}