using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_cleaned.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "certPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // -------------------------------------------------
            // 1. Add a digital signature (core API, no Facades)
            // -------------------------------------------------
            // Define the rectangle where the signature appearance will be placed
            Rectangle sigRect = new Rectangle(100, 100, 200, 150);

            // Create a signature field on the first page
            SignatureField signature = new SignatureField(doc.Pages[1], sigRect);
            signature.PartialName = "Signature1";

            // Add the signature field to the document's form collection (specify page number)
            doc.Form.Add(signature, 1);

            // Load the PKCS#12 certificate and apply the signature
            PKCS7 pkcs = new PKCS7(certPath, certPassword);
            // Set signature metadata on the PKCS7 object (SignatureField has no Reason/Location/ContactInfo)
            pkcs.Reason = "Document approved";
            pkcs.Location = "";          // empty to suppress location text
            pkcs.ContactInfo = "contact@example.com";

            // Sign the field
            signature.Sign(pkcs);

            // -------------------------------------------------
            // 2. Remove all annotations while preserving the signature
            // -------------------------------------------------
            // The signature is a form field, not a page annotation, so we can safely delete all annotations.
            foreach (Page page in doc.Pages)
            {
                // Iterate backwards because we are deleting items
                for (int i = page.Annotations.Count; i >= 1; i--)
                {
                    page.Annotations.Delete(i);
                }
            }

            // -------------------------------------------------
            // 3. Save the signed PDF with annotations removed
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}' with annotations removed.");
    }
}
