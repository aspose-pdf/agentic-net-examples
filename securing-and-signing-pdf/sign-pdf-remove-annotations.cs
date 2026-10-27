using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath   = "input.pdf";
        const string signedPdfPath  = "signed.pdf";
        const string cleanedPdfPath = "cleaned.pdf";

        // Certificate for signing – replace with your own file and password
        const string certPath     = "certificate.pfx";
        const string certPassword = "certPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // -------------------------------------------------
        // 1. Load the original PDF and add a digital signature
        // -------------------------------------------------
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a signature field on the first page
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 250, 150);
            SignatureField signatureField = new SignatureField(doc, sigRect);
            doc.Form.Add(signatureField, 1); // Add to page 1

            // Configure PKCS#1 signature using the provided certificate
            PKCS1 pkcsSignature = new PKCS1(certPath, certPassword);
            pkcsSignature.Location    = "Location";
            pkcsSignature.ContactInfo = "Contact";
            pkcsSignature.Reason      = "Document signed";

            // Sign the field
            signatureField.Sign(pkcsSignature);

            // Save the signed PDF
            doc.Save(signedPdfPath);
        }

        // -------------------------------------------------
        // 2. Load the signed PDF and remove all annotations
        //    except the signature widget, preserving the signature
        // -------------------------------------------------
        using (Document signedDoc = new Document(signedPdfPath))
        {
            // Iterate through all pages
            for (int pageIndex = 1; pageIndex <= signedDoc.Pages.Count; pageIndex++)
            {
                Page page = signedDoc.Pages[pageIndex];

                // Annotations collection is 1‑based; iterate backwards to allow safe removal
                for (int annIndex = page.Annotations.Count; annIndex >= 1; annIndex--)
                {
                    Annotation annotation = page.Annotations[annIndex];

                    // Preserve only signature widget annotations. The widget is represented by a SignatureField annotation.
                    // If the concrete type name is not "SignatureField", delete it.
                    if (annotation.GetType().Name != "SignatureField")
                    {
                        page.Annotations.Delete(annIndex);
                    }
                }
            }

            // Save the cleaned PDF (signature remains valid)
            signedDoc.Save(cleanedPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to: {signedPdfPath}");
        Console.WriteLine($"Cleaned PDF saved to: {cleanedPdfPath}");
    }
}
