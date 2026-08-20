using System;
using System.IO;
using Aspose.Pdf;                         // Core PDF classes
using Aspose.Pdf.Facades;                 // Facade classes (FormEditor, PdfFileSignature)
using Aspose.Pdf.Forms;                   // Signature types (PKCS1, PKCS7, etc.)

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and certificate files
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";
        const string appearanceImage = "signature_appearance.png"; // optional appearance image

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // ------------------------------------------------------------
            // 1. Add a signature field named "DigitalSignature" to page 1
            // ------------------------------------------------------------
            // FormEditor works directly on a Document instance.
            FormEditor formEditor = new FormEditor(doc);

            // Define the rectangle where the signature field will appear.
            // Coordinates are in points (1/72 inch). Adjust as needed.
            Rectangle fieldRect = new Rectangle(100, 500, 300, 550); // llx, lly, urx, ury

            // Add the signature field (FieldType.Signature) to page 1.
            // The AddField overload expects float values for the coordinates, so cast accordingly.
            bool fieldAdded = formEditor.AddField(
                FieldType.Signature,
                "DigitalSignature",
                1,
                (float)fieldRect.LLX,
                (float)fieldRect.LLY,
                (float)fieldRect.URX,
                (float)fieldRect.URY);

            if (!fieldAdded)
            {
                Console.Error.WriteLine("Failed to add the signature field.");
                return;
            }

            // ------------------------------------------------------------
            // 2. Prepare the digital signature
            // ------------------------------------------------------------
            // Create a PKCS1 signature object using the certificate.
            PKCS1 pkcs1Signature = new PKCS1(certPath, certPass)
            {
                Reason      = "Document approved",
                ContactInfo = "contact@example.com",
                Location    = "Office"
            };

            // ------------------------------------------------------------
            // 3. Sign the PDF using PdfFileSignature facade
            // ------------------------------------------------------------
            PdfFileSignature pdfSigner = new PdfFileSignature();

            // Bind the existing Document instance (no need to load from file again)
            pdfSigner.BindPdf(doc);

            // Optional: set a graphic appearance for the visible signature.
            // This should be a path to an image file (e.g., a scanned signature).
            if (File.Exists(appearanceImage))
                pdfSigner.SignatureAppearance = appearanceImage;

            // Set the certificate (required before calling Sign)
            pdfSigner.SetCertificate(certPath, certPass);

            // Sign the previously added field by its name.
            pdfSigner.Sign("DigitalSignature", pkcs1Signature);

            // Save the signed PDF to the output path.
            pdfSigner.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
