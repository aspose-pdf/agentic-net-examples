using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, the certificate (PFX) and the signed output PDF.
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";

        // Ensure the input files exist.
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear.
            // Constructor: (llx, lly, urx, ury) – lower‑left and upper‑right corners.
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field and add it to the document's form.
            SignatureField sigField = new SignatureField(doc, sigRect)
            {
                PartialName = "Signature1"   // Field name.
            };
            doc.Form.Add(sigField);

            // Create a PKCS#7 signature object using the PFX file.
            PKCS7 pkcs7Signature = new PKCS7(certificatePath, certificatePassword)
            {
                Reason      = "I agree to the terms.",
                Location    = "New York, USA",
                ContactInfo = "signer@example.com"
            };

            // Sign the document using the created signature field.
            sigField.Sign(pkcs7Signature);

            // Mark the document so that further signatures can be added via incremental updates.
            doc.Form.SignaturesAppendOnly = true;

            // Save the signed PDF. The default Save() writes an incremental update,
            // preserving the ability to add more signatures later.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Document signed successfully. Output saved to '{outputPdfPath}'.");
    }
}