using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the visible signature field will appear
            // (left, bottom, right, top) – coordinates are in points
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 600);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                Name = "MySignature",
                AlternateName = "Signature Field"
            };

            // Add the field's visual appearance to page 1
            doc.Form.AddFieldAppearance(sigField, 1, sigRect);

            // Create a concrete PKCS7 signature object from a PFX certificate
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document approved",
                Location = "Head Office",
                ContactInfo = "contact@example.com"
            };

            // Sign the document using the created signature field
            sigField.Sign(pkcs7);

            // Prevent further modifications after signing
            // Setting this flag forces incremental updates only, effectively locking the PDF
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed and locked PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
