using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and self‑signed certificate (PFX) paths
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "signed_output.pdf";
        const string certificatePath = "selfsigned.pfx";
        const string certificatePassword = "pfxPassword";

        // Verify that required files exist
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

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdfPath))
        {
            // Define the rectangle where the signature field will appear
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a signature field and add it to the document's form collection
            SignatureField sigField = new SignatureField(doc, sigRect);
            doc.Form.Add(sigField);

            // Create a PKCS#1 signature object using the self‑signed certificate
            // The constructor (string pfx, string password) loads the certificate from file
            PKCS1 pkcs1Signature = new PKCS1(certificatePath, certificatePassword)
            {
                // Optional metadata for the signature appearance
                Reason   = "Document approved",
                Location = "Office",
                ContactInfo = "admin@example.com",
                Date = DateTime.Now
            };

            // Sign the document using the created signature field
            sigField.Sign(pkcs1Signature);

            // Save the signed PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}