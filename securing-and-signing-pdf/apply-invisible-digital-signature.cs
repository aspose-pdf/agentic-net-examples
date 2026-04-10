using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_invisible.pdf";
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

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Append signatures incrementally so the original layout is untouched
            doc.Form.SignaturesAppendOnly = true;

            // Create an invisible signature field (zero‑size rectangle) on the first page
            var invisibleRect = new Rectangle(0, 0, 0, 0);
            var sigField = new SignatureField(doc, invisibleRect)
            {
                Name = "InvisibleSignature",
                PartialName = "InvisibleSignature"
            };
            doc.Form.Add(sigField);

            // Build a PKCS#7 signature using the supplied PFX certificate
            var pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                // Record the signing time
                Date = DateTime.Now,
                // No visual appearance – keep the signature invisible
                ShowProperties = false
            };

            // Apply the signature to the invisible field
            sigField.Sign(pkcs7);

            // Save the signed PDF (incremental update preserves original layout)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Invisible digital signature applied and saved to '{outputPdf}'.");
    }
}
