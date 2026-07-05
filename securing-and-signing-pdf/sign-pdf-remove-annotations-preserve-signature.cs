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
        const string certPath   = "certificate.pfx";   // signing certificate
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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Disable automatic signature sanitization so that the signature remains valid
            // after we modify the document (remove other annotations).
            doc.EnableSignatureSanitization = false;

            // -----------------------------------------------------------------
            // 1) Create a signature field on the first page.
            // -----------------------------------------------------------------
            // Define the rectangle where the visual signature will appear.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field and add it to the document's form.
            SignatureField signatureField = new SignatureField(doc, sigRect)
            {
                PartialName = "Signature1"
            };
            doc.Form.Add(signatureField);

            // -----------------------------------------------------------------
            // 2) Sign the document using a PKCS#7 signature.
            // -----------------------------------------------------------------
            // The PKCS7 class represents a standard digital signature.
            Signature pkcs7Signature = new PKCS7(certPath, certPass);
            signatureField.Sign(pkcs7Signature);

            // -----------------------------------------------------------------
            // 3) Remove all annotations except the signature field.
            // -----------------------------------------------------------------
            // Annotations collection uses 1‑based indexing. Iterate backwards
            // to safely delete items while iterating.
            foreach (Page page in doc.Pages)
            {
                for (int idx = page.Annotations.Count; idx >= 1; idx--)
                {
                    Annotation ann = page.Annotations[idx];
                    // Preserve the signature field; delete everything else.
                    if (!(ann is SignatureField))
                    {
                        page.Annotations.Delete(idx);
                    }
                }
            }

            // -----------------------------------------------------------------
            // 4) Save the signed PDF without the other annotations.
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}