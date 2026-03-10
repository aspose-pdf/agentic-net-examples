using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        // Create the PDF signature facade and bind the source PDF
        Aspose.Pdf.Facades.PdfFileSignature pdfSigner = new Aspose.Pdf.Facades.PdfFileSignature();
        pdfSigner.BindPdf(inputPdf);

        // Optional: set a visible appearance image for the signature
        // pdfSigner.SignatureAppearance = "signature_image.jpg";

        // Create a PKCS7 (hash‑based) signature object using the certificate
        Aspose.Pdf.Forms.PKCS7 pkcs7Signature = new Aspose.Pdf.Forms.PKCS7(pfxPath, pfxPassword);
        pkcs7Signature.Reason = "Document approved";
        pkcs7Signature.ContactInfo = "contact@example.com";
        pkcs7Signature.Location = "Head Office";

        // Define the rectangle where the signature will appear on page 1
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Sign page 1 with a visible signature
        pdfSigner.Sign(1, "Signing Reason", "contact@example.com", "Head Office", true, rect, pkcs7Signature);

        // Save the signed PDF
        pdfSigner.Save(outputPdf);

        // Release resources
        pdfSigner.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}