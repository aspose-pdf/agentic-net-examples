using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Signatures;
using Aspose.Pdf.Annotations; // Added namespace for AnnotationFlags

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_flattened.pdf";
        const string pfxPath       = "certificate.pfx";
        const string pfxPassword   = "pfxPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 150);

            // Create a signature field on the first page
            SignatureField sigField = new SignatureField(doc.Pages[1], sigRect);
            sigField.Name = "Signature1";
            sigField.PartialName = "Signature1";
            sigField.Flags = AnnotationFlags.Print; // optional: make it printable

            // Add the signature field to the page annotations
            doc.Pages[1].Annotations.Add(sigField);

            // Load the certificate (PFX) for signing
            using (FileStream pfxStream = File.OpenRead(pfxPath))
            {
                // Create a PKCS7 signature object
                PKCS7 pkcs7Signature = new PKCS7(pfxStream, pfxPassword)
                {
                    Reason = "Document approved",
                    Location = "Office",
                    ContactInfo = "contact@example.com",
                    Authority = "John Doe"
                };

                // Sign the document using the signature field
                sigField.Sign(pkcs7Signature);
            }

            // Flatten all form fields so they become part of the page content
            doc.Flatten();

            // Save the signed and flattened PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed and flattened PDF saved to '{outputPdfPath}'.");
    }
}
