using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";          // source PDF with a signature field
        const string xmlPath      = "signatureData.xml";  // XFDF (XML) containing field values
        const string pfxPath      = "certificate.pfx";    // signing certificate
        const string pfxPassword  = "pfxPassword";        // certificate password
        const string outputPath   = "signed_output.pdf";  // result PDF

        // Verify required files exist
        if (!File.Exists(pdfPath) || !File.Exists(xmlPath) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("One or more required files are missing.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(pdfPath))
        {
            // Import field values from the XFDF (XML) file into the PDF.
            using (FileStream xmlStream = File.OpenRead(xmlPath))
            {
                XfdfReader.ReadFields(xmlStream, doc);
            }

            // Locate the signature field by its name (adjust the name as needed).
            SignatureField sigField = doc.Form["Signature1"] as SignatureField;
            if (sigField == null)
            {
                Console.Error.WriteLine("Signature field 'Signature1' not found.");
                // Save the document without signing (optional)
                doc.Save(outputPath);
                return;
            }

            // Create a concrete PKCS7 signature object using the PFX certificate.
            // Signature is abstract; PKCS7 (or PKCS7Detached) provides a concrete implementation.
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                PKCS7 pkcs7 = new PKCS7(pfxStream, pfxPassword);
                pkcs7.Reason   = "Document approved";
                pkcs7.Location = "Head Office";
                // Optional: additional metadata
                // pkcs7.ContactInfo = "contact@example.com";

                // Apply the signature to the field.
                sigField.Sign(pkcs7);
            }

            // Save the signed PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
