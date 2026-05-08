using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // XfdfReader resides here
using Aspose.Pdf.Forms;        // SignatureField, PKCS7, etc.

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";          // PDF containing a signature field
        const string xfdfPath     = "signature_data.xfdf"; // XFDF (XML) with signature field values
        const string pfxPath      = "certificate.pfx";   // Certificate for signing
        const string pfxPassword  = "pfxPassword";        // Password for the PFX file
        const string outputPdf    = "signed_output.pdf";

        // Verify input files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(xfdfPath))
        {
            Console.Error.WriteLine($"XFDF not found: {xfdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(pdfPath))
        {
            // Import field values (including signature field data) from the XFDF XML file
            using (FileStream xfdfStream = File.OpenRead(xfdfPath))
            {
                XfdfReader.ReadFields(xfdfStream, doc);
            }

            // Locate the signature field by its full name.
            // Adjust "Signature1" to match the actual field name in your PDF.
            const string signatureFieldName = "Signature1";
            SignatureField sigField = doc.Form[signatureFieldName] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine($"Signature field '{signatureFieldName}' not found.");
                return;
            }

            // Create a concrete PKCS7 signature object using the certificate (PFX) and password.
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Apply the digital signature to the field.
            sigField.Sign(pkcs7);

            // Save the signed PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
