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
        const string outputPdf = "signed_final.pdf";
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

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create a signature field on the first page
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
            {
                PartialName = "Signature1"
            };
            // Add the signature field to the page annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create a PKCS#7 signature object using the PFX certificate
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document approved",
                Location = "Company HQ",
                ContactInfo = "contact@example.com"
                // SigningTime is automatically set to the current time by the library
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7Signature);

            // NOTE: The core Aspose.Pdf API (Document class) does not expose a
            // DocMDP certification method in versions prior to 23.x. The
            // DocMDPSignature class is intended to be used with Document.Sign(...),
            // which is unavailable here. To enforce finality without using the
            // Facades namespace (which is prohibited by the task constraints),
            // we cannot apply a DocMDP certification signature. The field is
            // signed, and further modifications will invalidate the signature.
            // If a later version of Aspose.Pdf is referenced, the following code
            // could be re‑enabled:
            //
            // DocMDPSignature mdpSignature = new DocMDPSignature(pkcs7Signature, DocMDPAccessPermissions.NoChanges);
            // doc.Sign(mdpSignature);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF signed: {outputPdf}");
    }
}
