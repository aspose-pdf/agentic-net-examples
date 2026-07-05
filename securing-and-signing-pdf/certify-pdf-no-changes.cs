using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "certified.pdf";
        const string pfxPath   = "certificate.pfx";
        const string pfxPassword = "password";

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
            // Create a PKCS#1 signature object from the certificate
            PKCS1 pkcs1 = new PKCS1(pfxPath, pfxPassword)
            {
                Reason      = "Document certified – no changes allowed",
                ContactInfo = "signer@example.com",
                Location    = "New York"
            };

            // Create a DocMDP signature that disallows any further modifications
            DocMDPSignature mdpSignature = new DocMDPSignature(pkcs1, DocMDPAccessPermissions.NoChanges);

            // Define the rectangle where the (invisible) certification signature will be placed
            // Use System.Drawing.Rectangle because PdfFileSignature.Certify expects this type
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 0, 0); // invisible

            // Use the Facade class via its fully‑qualified name (no using directive for Aspose.Pdf.Facades)
            using (Aspose.Pdf.Facades.PdfFileSignature signer = new Aspose.Pdf.Facades.PdfFileSignature(doc))
            {
                // Certify the document on the first page (page numbers are 1‑based)
                signer.Certify(
                    page: 1,
                    SigReason: pkcs1.Reason,
                    SigContact: pkcs1.ContactInfo,
                    SigLocation: pkcs1.Location,
                    visible: false,
                    annotRect: rect,
                    docMdpSignature: mdpSignature);

                // Save the certified PDF
                signer.Save(outputPdf);
            }

            Console.WriteLine($"Certified PDF saved to '{outputPdf}'.");
        }
    }
}
