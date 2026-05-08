using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPdf = "input.pdf";
        string outputPdf = "signed_output.pdf";
        string certPath = "certificate.pfx";
        string certPassword = "password";

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

        // Bind the PDF to the signature facade
        PdfFileSignature pdfSign = new PdfFileSignature();
        pdfSign.BindPdf(inputPdf);

        // Create a PKCS7 signature object
        PKCS7 pkcs7 = new PKCS7(certPath, certPassword);
        pkcs7.Reason = "Approved";
        pkcs7.ContactInfo = "signer@example.com";
        pkcs7.Location = "New York";

        // Configure semi‑transparent background for the signature appearance
        SignatureCustomAppearance appearance = new SignatureCustomAppearance();
        // 50% transparent light gray background (alpha = 128)
        appearance.BackgroundColor = Aspose.Pdf.Color.FromArgb(128, 200, 200, 200);
        pkcs7.CustomAppearance = appearance;

        // Define the signature rectangle (System.Drawing.Rectangle)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Apply the signature on page 1, make it visible
        pdfSign.Sign(1, pkcs7.Reason, pkcs7.ContactInfo, pkcs7.Location, true, rect, pkcs7);

        // Save the signed PDF
        pdfSign.Save(outputPdf);
        pdfSign.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}