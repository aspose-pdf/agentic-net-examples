using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF
        const string outputPdfPath = "signed_clean.pdf";   // result PDF
        const string certificatePath = "certificate.pfx"; // signing certificate
        const string certificatePassword = "password";    // certificate password

        // Verify required files exist
        if (!File.Exists(inputPdfPath) || !File.Exists(certificatePath))
        {
            Console.Error.WriteLine("Input PDF or certificate file not found.");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Prevent automatic sanitization that could invalidate the signature after modifications
            doc.EnableSignatureSanitization = false;

            // -----------------------------------------------------------------
            // 1. Add a signature field (if needed) and sign the document
            // -----------------------------------------------------------------
            Page firstPage = doc.Pages[1]; // 1‑based indexing

            // Define the rectangle where the signature will appear
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

            // Create the signature field on the first page
            SignatureField signatureField = new SignatureField(firstPage, sigRect)
            {
                PartialName = "Signature1" // optional field name
            };
            firstPage.Annotations.Add(signatureField);

            // Create a PKCS7 signature object using the certificate
            using (FileStream certStream = File.OpenRead(certificatePath))
            {
                PKCS7 pkcs7 = new PKCS7(certStream, certificatePassword)
                {
                    Reason = "Document signed",
                    Location = "Office"
                };

                // Sign the document using the signature field
                signatureField.Sign(pkcs7);
            }

            // -----------------------------------------------------------------
            // 2. Remove all annotations except the signature annotation
            // -----------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                // Collect 1‑based indices of annotations that are NOT signature fields
                List<int> indicesToRemove = new List<int>();
                for (int i = 1; i <= page.Annotations.Count; i++)
                {
                    Annotation ann = page.Annotations[i];
                    // SignatureField derives from Annotation, so we keep it; everything else is removed
                    if (!(ann is SignatureField))
                    {
                        indicesToRemove.Add(i);
                    }
                }

                // Delete collected annotations in reverse order to keep indices valid
                for (int i = indicesToRemove.Count - 1; i >= 0; i--)
                {
                    page.Annotations.Delete(indicesToRemove[i]);
                }
            }

            // -----------------------------------------------------------------
            // 3. Save the signed PDF with annotations removed
            // -----------------------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
