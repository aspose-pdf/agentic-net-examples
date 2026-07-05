using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures;

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
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle where the signature field will appear (left, bottom, right, top)
            Rectangle rect = new Rectangle(100, 100, 300, 200);

            // Add a signature field to the first page (page numbers are 1‑based)
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);
            sigField.PartialName = "Signature1";
            doc.Form.Add(sigField, 1);

            // Create a PKCS#7 signature object using the certificate file and password
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);

            // Optional: set signature metadata (these properties belong to PKCS7)
            pkcs7.Reason = "Document approval";
            pkcs7.Location = "Head Office";
            pkcs7.ContactInfo = "contact@example.com";
            // The signing date is set automatically; the SignDate property does not exist in recent versions.

            // Sign the PDF using the signature field
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
