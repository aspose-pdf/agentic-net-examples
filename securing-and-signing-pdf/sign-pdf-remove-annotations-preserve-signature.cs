using System;
using System.Collections.Generic;
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
        const string pfxPath    = "certificate.pfx";    // signing certificate
        const string pfxPassword = "password";          // certificate password

        if (!File.Exists(inputPdf) || !File.Exists(pfxPath))
        {
            Console.Error.WriteLine("Input PDF or certificate not found.");
            return;
        }

        // Load the PDF document (lifecycle: using ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Prevent automatic sanitization that could invalidate the signature after modifications
            doc.EnableSignatureSanitization = false;

            // -----------------------------------------------------------------
            // 1. Create a signature field on the first page
            // -----------------------------------------------------------------
            // Fully qualified rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                PartialName = "Signature1"
            };

            // -----------------------------------------------------------------
            // 2. Prepare the signature object (PKCS7)
            // -----------------------------------------------------------------
            // PKCS7 can be constructed from a file path or a stream; here we use the file path
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword)
            {
                Reason   = "Document signed",
                Location = "Office"
            };

            // -----------------------------------------------------------------
            // 3. Sign the document using the signature field
            // -----------------------------------------------------------------
            sigField.Sign(pkcs7);

            // -----------------------------------------------------------------
            // 4. Remove all annotations except the signature field
            // -----------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                // Collect annotations that are NOT the signature field
                List<Annotation> toDelete = new List<Annotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (!(ann is SignatureField))
                    {
                        toDelete.Add(ann);
                    }
                }

                // Delete the collected annotations
                foreach (Annotation ann in toDelete)
                {
                    page.Annotations.Delete(ann);
                }
            }

            // -----------------------------------------------------------------
            // 5. Save the signed PDF with annotations removed
            // -----------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}