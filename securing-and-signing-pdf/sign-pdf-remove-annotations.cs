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
        const string outputPath = "signed_clean.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

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
            // Optional: remove PDF/A and PDF/UA compliance to avoid invalidating the signature after modifications
            doc.RemovePdfaCompliance();
            doc.RemovePdfUaCompliance();

            // Create a signature field on the first page
            Page page = doc.Pages[1];
            Rectangle sigRect = new Rectangle(100, 100, 300, 200);
            SignatureField sigField = new SignatureField(page, sigRect)
            {
                PartialName = "Signature1"
            };
            // Add the field to the document's form collection (required for signing)
            doc.Form.Add(sigField);

            // Prepare the digital signature using a PFX certificate (concrete PKCS7 class)
            PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
            {
                Reason = "Document approved",
                ContactInfo = "contact@example.com",
                Location = "Office"
            };

            // Sign the document using the created signature field
            sigField.Sign(pkcs7);

            // Remove all annotations (e.g., comments, highlights) while preserving the signature field
            foreach (Page pg in doc.Pages)
            {
                // Iterate backwards because deleting changes the collection size
                for (int i = pg.Annotations.Count; i >= 1; i--)
                {
                    Annotation ann = pg.Annotations[i];
                    // Signature fields are form fields, not regular annotations, so we can delete everything here
                    pg.Annotations.Delete(i);
                }
            }

            // Save the signed PDF with annotations removed
            doc.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}' with annotations removed.");
    }
}
