using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string xmlPath      = "input.xml";          // Source XML file
        const string outputPdf    = "signed_output.pdf"; // Resulting signed PDF
        const string certPath     = "certificate.pfx";   // PKCS#12 certificate file
        const string certPassword = "yourPassword";      // Certificate password

        // Ensure the source files exist
        if (!File.Exists(xmlPath))
        {
            Console.Error.WriteLine($"XML file not found: {xmlPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Create a new PDF document and bind the XML content to it
        using (Document doc = new Document())
        {
            // BindXml loads the XML and creates PDF pages based on its structure
            doc.BindXml(xmlPath);

            // Define the rectangle where the visible signature will appear
            // Rectangle constructor: (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create a signature field on the first page (page index is 1‑based)
            // The constructor (Document, Rectangle) places the field on page 1 by default
            SignatureField sigField = new SignatureField(doc, sigRect);

            // Add the signature field to the document's form collection
            doc.Form.Add(sigField);

            // Create a concrete signature object (PKCS7) using the certificate file
            PKCS7 pkcs7Signature = new PKCS7(certPath, certPassword);
            pkcs7Signature.Reason      = "Document approved";
            pkcs7Signature.ContactInfo = "signer@example.com";
            pkcs7Signature.Location    = "New York";

            // Sign the field. The PKCS7 instance already contains the certificate,
            // so we can use the overload that takes only the Signature object.
            sigField.Sign(pkcs7Signature);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}