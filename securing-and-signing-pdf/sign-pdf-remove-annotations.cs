using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "signed_clean.pdf";   // result PDF
        const string certPath   = "certificate.pfx";    // signing certificate
        const string certPass   = "password";           // certificate password

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Prevent automatic sanitization that could invalidate the signature after modifications
            doc.EnableSignatureSanitization = false;

            // -------------------------------------------------
            // 1. Add a signature field to the first page
            // -------------------------------------------------
            Page firstPage = doc.Pages[1];
            // Define the rectangle where the signature will appear
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 200, 150);
            // Create the signature field and add it to the page annotations
            SignatureField sigField = new SignatureField(firstPage, sigRect);
            firstPage.Annotations.Add(sigField);

            // -------------------------------------------------
            // 2. Sign the document using a PKCS#7 signature
            // -------------------------------------------------
            // The PKCS7 constructor accepts the path to a PFX file and its password
            PKCS7 pkcs7Signature = new PKCS7(certPath, certPass);
            // Optional: set signature properties (reason, location, etc.)
            pkcs7Signature.Reason = "Document approved";
            pkcs7Signature.Location = "Office";
            // Apply the signature to the field
            sigField.Sign(pkcs7Signature);

            // -------------------------------------------------
            // 3. Remove all annotations except the signature field
            // -------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                // Annotation collections are 1‑based; iterate backwards when deleting
                for (int idx = page.Annotations.Count; idx >= 1; idx--)
                {
                    Annotation ann = page.Annotations[idx];
                    // Preserve the signature field; delete everything else
                    if (!(ann is SignatureField))
                    {
                        page.Annotations.Delete(idx);
                    }
                }
            }

            // -------------------------------------------------
            // 4. Save the signed PDF without the other annotations
            // -------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}