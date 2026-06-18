using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Added for Annotation base class

class Program
{
    static void Main()
    {
        const string inputPdf = "filled_form.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Try to locate an existing signature field
            SignatureField sigField = null;
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is SignatureField sf)
                    {
                        sigField = sf;
                        break;
                    }
                }
                if (sigField != null) break;
            }

            // If no signature field exists, create one on the first page
            if (sigField == null)
            {
                // Define the rectangle for the signature appearance:
                // left = 100, bottom = 100, width = 150, height = 50
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 100, 150, 50);
                // Create the signature field on page 1
                sigField = new SignatureField(doc.Pages[1], rect);
                // Add the field to the page's annotation collection
                doc.Pages[1].Annotations.Add(sigField);
            }

            // Create a concrete PKCS7 signature object from the PFX certificate
            PKCS7 signature = new PKCS7(pfxPath, pfxPassword);
            // Optional: set additional signature properties
            signature.Reason = "Document certification";
            signature.Location = "Company HQ";
            signature.ContactInfo = "contact@example.com";

            // Apply the digital signature using the signature field
            sigField.Sign(signature);

            // Save the signed PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
