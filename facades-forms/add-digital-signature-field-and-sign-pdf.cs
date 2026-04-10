using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using System.Drawing; // for Rectangle (System.Drawing.Rectangle)

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPass = "password";
        const string appearanceImage = "signature.png";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // ------------------------------------------------------------
            // 1. Add a signature field named "DigitalSignature" to page 1
            // ------------------------------------------------------------
            FormEditor formEditor = new FormEditor(doc);
            // Define the rectangle where the signature field will appear (llx, lly, urx, ury)
            float llx = 100f, lly = 100f, urx = 300f, ury = 150f;
            bool fieldAdded = formEditor.AddField(FieldType.Signature, "DigitalSignature", 1, llx, lly, urx, ury);
            if (!fieldAdded)
            {
                Console.Error.WriteLine("Failed to add signature field.");
                return;
            }

            // ------------------------------------------------------------
            // 2. Prepare the PKCS7 signature object with desired properties
            // ------------------------------------------------------------
            PKCS7 pkcs7 = new PKCS7(certPath, certPass);
            pkcs7.Reason = "Document approval";
            pkcs7.Location = "New York";
            pkcs7.ContactInfo = "john.doe@example.com"; // correct property name

            // ------------------------------------------------------------
            // 3. Sign the document using PdfFileSignature facade
            // ------------------------------------------------------------
            PdfFileSignature pdfSigner = new PdfFileSignature(doc);
            pdfSigner.SetCertificate(certPath, certPass);
            // Optional graphic appearance for the signature field
            pdfSigner.SignatureAppearance = appearanceImage;
            // Sign using the previously added field name
            pdfSigner.Sign("DigitalSignature", pkcs7);

            // ------------------------------------------------------------
            // 4. Save the signed PDF
            // ------------------------------------------------------------
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
