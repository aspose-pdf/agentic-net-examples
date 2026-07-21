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
        // Input PDF, output PDF and signing certificate details
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_clean.pdf";
        const string pfxPath        = "certificate.pfx";
        const string pfxPassword    = "pfxPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Disable automatic signature sanitization to keep the signature after modifications
            doc.EnableSignatureSanitization = false;

            // -------------------------------------------------
            // 1. Add a signature field and sign the document
            // -------------------------------------------------
            // Choose the first page for the signature field
            Page firstPage = doc.Pages[1];

            // Define the rectangle where the signature will appear
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the signature field and add it to the page annotations
            SignatureField sigField = new SignatureField(firstPage, sigRect);
            firstPage.Annotations.Add(sigField);

            // Create a PKCS7 signature object using the PFX file
            PKCS7 pkcs7Signature = new PKCS7(pfxPath, pfxPassword)
            {
                Reason   = "Document approved",
                Location = "Office",
                ContactInfo = "contact@example.com",
                Date = DateTime.UtcNow
            };

            // Sign the document using the signature field
            sigField.Sign(pkcs7Signature);

            // -------------------------------------------------
            // 2. Remove all annotations except the signature field
            // -------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                // Collect annotations that are NOT signature fields
                List<Annotation> toRemove = new List<Annotation>();
                foreach (Annotation ann in page.Annotations)
                {
                    if (!(ann is SignatureField))
                        toRemove.Add(ann);
                }

                // Delete the collected annotations
                foreach (Annotation ann in toRemove)
                {
                    page.Annotations.Delete(ann);
                }
            }

            // -------------------------------------------------
            // 3. Save the signed PDF without the other annotations
            // -------------------------------------------------
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}