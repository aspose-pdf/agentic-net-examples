using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf;

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

        // Bind the PDF document for signing
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);

            // Create a PKCS7 signature object
            PKCS7 pkcs7 = new PKCS7(pfxPath, pfxPassword);
            pkcs7.Reason = "Document approval";
            pkcs7.ContactInfo = "signer@example.com";
            pkcs7.Location = "New York";

            // Configure custom appearance with a semi‑transparent background
            SignatureCustomAppearance appearance = new SignatureCustomAppearance();
            // 50% transparent blue background (alpha 128)
            appearance.BackgroundColor = Aspose.Pdf.Color.FromArgb(128, 0, 0, 255);
            pkcs7.CustomAppearance = appearance;

            // Define the signature rectangle (System.Drawing.Rectangle)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Apply the signature on page 1
            pdfSign.Sign(1, pkcs7.Reason, pkcs7.ContactInfo, pkcs7.Location, true, rect, pkcs7);

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}