using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePwd = "certPassword";
        const string signatureImg = "signature.png";

        const string reason = "Approved for release";
        const string location = "New York, USA";
        const string contactInfo = "john.doe@example.com";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }
        if (!File.Exists(signatureImg))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImg}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdf);

        // Define the rectangle where the signature will be placed (first page)
        // Use the PDF‑specific Rectangle type (Aspose.Pdf.Rectangle) – not the drawing one.
        Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(100, 100, 300, 200);

        // Create a signature field on the first page
        SignatureField sigField = new SignatureField(doc.Pages[1], sigRect)
        {
            Name = "Signature1"
        };
        doc.Form.Add(sigField);

        // Prepare PKCS#7 signature and assign reason, location, contact info
        PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePwd)
        {
            Reason = reason,
            Location = location,
            ContactInfo = contactInfo
        };

        // Sign the field
        sigField.Sign(pkcs7);

        // Add visual appearance (signature image) over the signature field
        ImageStamp imgStamp = new ImageStamp(signatureImg)
        {
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom,
            XIndent = sigRect.LLX,
            YIndent = sigRect.LLY,
            Width = sigRect.URX - sigRect.LLX,
            Height = sigRect.URY - sigRect.LLY,
            Background = false // draw in foreground
        };
        // Place the stamp on the same page that contains the signature field (page 1)
        doc.Pages[1].AddStamp(imgStamp);

        // Save the signed PDF to a new file
        doc.Save(outputPdf);

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}
