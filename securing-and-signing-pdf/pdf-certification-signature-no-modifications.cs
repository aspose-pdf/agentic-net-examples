using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";            // source PDF
        const string outputPdf  = "certified_output.pdf"; // signed PDF
        const string pfxPath    = "certificate.pfx";     // PKCS#12 file
        const string pfxPassword = "pfxPassword";        // password for the PFX

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Ensure that any existing signatures are treated as append‑only
            // (required for certification signatures)
            doc.Form.SignaturesAppendOnly = true;

            // Define the rectangle where the certification signature will appear
            // Use float values as required by Aspose.Pdf.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(
                (float)100,   // lower‑left X
                (float)500,   // lower‑left Y
                (float)300,   // upper‑right X
                (float)550    // upper‑right Y
            );

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                PartialName   = "CertSignature",
                AlternateName = "Document Certification"
            };

            // Add the signature field to the page's annotations collection
            doc.Pages[1].Annotations.Add(sigField);

            // Create a concrete PKCS#7 signature (Signature is abstract)
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason      = "Document certified – no changes allowed",
                Location    = "Company XYZ",
                ContactInfo = "contact@company.com",
                Authority   = "Company XYZ"
            };

            // Sign the field using the PKCS#7 signature.
            // Note: Core API does not expose a direct way to create a certification (DocMDP) signature.
            // The PKCS#7 signature will be applied; for a true certification signature the Facades
            // PdfFileSignature.Certify method would be required, which is outside the allowed namespaces.
            sigField.Sign(pkcs7);

            // Save the signed PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signature applied. Output saved to '{outputPdf}'.");
    }
}
