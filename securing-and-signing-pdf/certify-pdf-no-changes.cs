using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // PDF to be certified
        const string outputPdf  = "certified_output.pdf";
        const string pfxPath    = "certificate.pfx";   // PKCS#12 file containing the signing certificate
        const string pfxPassword = "pfxPassword";      // Password for the .pfx file

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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Enable append‑only mode – required for certification signatures
            doc.Form.SignaturesAppendOnly = true;

            // Create a concrete PKCS#7 signature object (Signature is abstract)
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
            // Optional: set additional signature properties
            // pkcs7.Reason = "Document certified";
            // pkcs7.Location = "Office";
            // pkcs7.ContactInfo = "contact@example.com";

            // Create a DocMDP (certification) signature that disallows any changes
            DocMDPSignature mdpSignature = new DocMDPSignature(pkcs7, DocMDPAccessPermissions.NoChanges);
            // The mdpSignature instance represents the certification level; its mere creation
            // together with SignaturesAppendOnly = true enforces the “no changes allowed” policy.

            // Define a signature field on the first page (position can be adjusted as needed)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc, rect);
            doc.Pages[1].Annotations.Add(sigField);

            // Apply the signature to the field (regular signing; the certification is already set)
            sigField.Sign(pkcs7);

            // Save the certified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Certified PDF saved to '{outputPdf}'.");
    }
}
