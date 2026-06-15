using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF to be certified
        const string outputPdf = "certified_output.pdf"; // Resulting PDF
        const string pfxPath = "certificate.pfx";   // PKCS#12 file containing the signing certificate
        const string pfxPassword = "pfxPassword";      // Password for the PFX file

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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPdf))
        {
            // Concrete signature implementation – PKCS#7 (Signature is abstract)
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document certified",
                Location = "Office",
                ContactInfo = "contact@example.com"
            };

            // Create a certification (MDP) signature that disallows any changes
            // DocMDPAccessPermissions.NoChanges enforces a "no changes allowed" policy
            DocMDPSignature mdpSignature = new DocMDPSignature(pkcs7, DocMDPAccessPermissions.NoChanges);

            // Add a visible signature field to the first page (optional positioning)
            // Rectangle: left, bottom, right, top
            Rectangle rect = new Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], rect);

            // Add the signature field to the page's annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Sign the field using the concrete PKCS7 instance.
            // The DocMDPSignature instance ensures the certification level is applied.
            sigField.Sign(pkcs7);

            // Save the certified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Certified PDF saved to '{outputPdf}'.");
    }
}
