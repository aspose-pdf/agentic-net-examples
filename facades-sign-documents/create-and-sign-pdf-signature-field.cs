using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath  = "certificate.pfx";
        const string certPass  = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle for the signature field (lower‑left x,y and upper‑right x,y)
            // Adjust these values as needed for your layout
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on page 1
            SignatureField sigField = new SignatureField(doc, fieldRect);
            sigField.Name = "Signature1";               // field name used later for signing
            sigField.AlternateName = "Signature Field"; // optional tooltip

            // Add the field to the page's annotation collection
            doc.Pages[1].Annotations.Add(sigField);

            // Prepare the facade for signing, binding it to the in‑memory document
            using (PdfFileSignature pdfSigner = new PdfFileSignature(doc))
            {
                // Set the certificate that will be used for the digital signature
                pdfSigner.SetCertificate(certPath, certPass);

                // Optional: set an image that will appear as the visual signature
                // pdfSigner.SignatureAppearance = "signatureImage.png";

                // Create a PKCS#1 signature object (you could also use PKCS7)
                PKCS1 pkcs1Signature = new PKCS1(certPath, certPass);
                pkcs1Signature.Reason   = "Approved";
                pkcs1Signature.ContactInfo = "john.doe@example.com";
                pkcs1Signature.Location = "New York";

                // Sign the document using the previously created signature field
                pdfSigner.Sign("Signature1", "Approved", "john.doe@example.com", "New York", pkcs1Signature);

                // Save the signed PDF
                pdfSigner.Save(outputPdf);
            }
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}