using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and signing certificate
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "pfxPassword";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: load)
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the visible signature field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field on the document (constructor overload Document, Rectangle)
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                // Set a name for the field (used as identifier)
                PartialName = "Signature1",
                // Optional tooltip shown in PDF viewers
                AlternateName = "Document Signature"
            };

            // Add the field to the form
            doc.Form.Add(sigField);

            // Add a visible appearance for the field on page 1
            // This renders the field rectangle so the user can see where to sign
            doc.Form.AddFieldAppearance(sigField, 1, sigRect);

            // Create a concrete PKCS#7 signature object using the PFX certificate
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Approved by author",
                Location = "Head Office",
                ContactInfo = "author@example.com"
            };

            // Sign the document using the created signature field
            sigField.Sign(pkcs7);

            // Lock the document after signing so further modifications invalidate the signature
            // Setting SignaturesAppendOnly forces incremental updates only (no content changes)
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed and locked PDF (lifecycle rule: save)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
