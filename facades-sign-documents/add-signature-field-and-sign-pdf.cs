using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and certificate details
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

        // Verify input files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Define the signature field rectangle (llx, lly, urx, ury)
        // Adjust these coordinates to the desired location on the page
        Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

        // Name of the signature field
        const string fieldName = "Signature1";

        // Load the PDF, add a signature field, and sign it
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page (page index is 1‑based)
            SignatureField sigField = new SignatureField(doc, sigRect);
            sigField.PartialName = fieldName; // set the field name

            // Add the field to the document's form collection
            doc.Form.Add(sigField);

            // Prepare the signature object (PKCS#1 in this example)
            PKCS1 signature = new PKCS1(certPath, certPass);
            signature.Reason   = "Approved";
            signature.ContactInfo = "signer@example.com";
            signature.Location = "New York";

            // Use PdfFileSignature facade to apply the signature to the created field
            PdfFileSignature pdfSigner = new PdfFileSignature();
            pdfSigner.BindPdf(doc);                     // bind the in‑memory document
            pdfSigner.SetCertificate(certPath, certPass);
            // Optional: set an image to appear as the visual signature
            // pdfSigner.SignatureAppearance = "signature_image.jpg";

            // Sign the document using the field name; rectangle is taken from the field itself
            pdfSigner.Sign(fieldName, signature);

            // Save the signed PDF
            pdfSigner.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}