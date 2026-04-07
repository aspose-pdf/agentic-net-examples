using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF that will receive the signature field
        const string outputPdf  = "signed_locked.pdf"; // Resulting PDF
        const string pfxPath    = "certificate.pfx";   // Path to signing certificate
        const string pfxPassword = "password";         // Certificate password

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

        // Load the document and add a required signature field
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle for the signature field (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create the signature field, give it a name and mark it as required
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                Name = "Signature1",
                Required = true
            };

            // Add the field to the document's form
            doc.Form.Add(sigField);

            // Prepare the PDF facade for signing
            using (PdfFileSignature pdfSign = new PdfFileSignature(doc))
            {
                // Create a PKCS#7 signature object with the certificate
                PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
                {
                    Reason = "Document approval",
                    ContactInfo = "signer@example.com",
                    Location = "New York"
                };

                // Create an MDP signature that locks the document (no changes allowed after signing)
                DocMDPSignature mdpSignature = new DocMDPSignature(pkcs7, DocMDPAccessPermissions.NoChanges);

                // Certify the document using the existing signature field.
                // This signs the field and applies the MDP restrictions.
                pdfSign.Certify("Signature1", mdpSignature);

                // Save the signed and locked PDF
                pdfSign.Save(outputPdf);
            }
        }

        Console.WriteLine($"Document signed, required, and locked saved to '{outputPdf}'.");
    }
}