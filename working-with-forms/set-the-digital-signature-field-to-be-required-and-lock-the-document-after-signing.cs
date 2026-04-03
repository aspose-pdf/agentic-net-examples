using System;
using System.IO;
using System.Drawing;                     // needed for System.Drawing.Rectangle
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "signed_locked.pdf"; // result PDF
        const string pfxPath   = "certificate.pfx";   // signing certificate
        const string pfxPass   = "password";          // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            // Aspose.Pdf.Rectangle uses PDF coordinates (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);
            SignatureField sigField = new SignatureField(doc.Pages[1], fieldRect);

            // Mark the field as required
            sigField.Required = true;

            // Add the field to the document's form collection
            doc.Form.Add(sigField);

            // Prepare the PDF signature facade bound to the same document
            using (PdfFileSignature pdfSignature = new PdfFileSignature(doc))
            {
                // Set the certificate for signing (optional when using PKCS1 object)
                pdfSignature.SetCertificate(pfxPath, pfxPass);

                // Create a PKCS1 signature object
                PKCS1 pkcs1 = new PKCS1(pfxPath, pfxPass);

                // Create an MDP (Modification Detection and Prevention) signature
                // NoChanges locks the document after signing (no further modifications allowed)
                DocMDPSignature mdpSignature = new DocMDPSignature(pkcs1, DocMDPAccessPermissions.NoChanges);

                // Define the visible rectangle for the signature appearance (System.Drawing.Rectangle)
                System.Drawing.Rectangle visibleRect = new System.Drawing.Rectangle(100, 100, 200, 50);

                // Certify (sign) the document with the MDP signature
                // Parameters: page number, reason, contact, location, visibility, rectangle, MDP signature
                pdfSignature.Certify(
                    page: 1,
                    SigReason: "Document approved",
                    SigContact: "contact@example.com",
                    SigLocation: "New York, USA",
                    visible: true,
                    annotRect: visibleRect,
                    docMdpSignature: mdpSignature);

                // Save the signed and locked PDF
                pdfSignature.Save(outputPdf);
            }
        }

        Console.WriteLine($"Signed and locked PDF saved to '{outputPdf}'.");
    }
}