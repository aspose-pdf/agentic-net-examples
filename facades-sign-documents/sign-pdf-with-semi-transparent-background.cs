using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output signed PDF and certificate details
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath  = "certificate.pfx";
        const string certPwd   = "password";

        // Ensure the input files exist
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

        // Create a PKCS#1 signature object and configure its appearance
        Aspose.Pdf.Forms.PKCS1 pkcs1 = new Aspose.Pdf.Forms.PKCS1(certPath, certPwd);
        // Set reason, contact and location (optional, but often required)
        pkcs1.Reason   = "Document approval";
        pkcs1.ContactInfo = "john.doe@example.com";
        pkcs1.Location = "New York";

        // Create a custom appearance and set a semi‑transparent background color.
        // Aspose.Pdf.Color does not expose an alpha channel directly; using a light gray
        // provides a visually softer background that improves readability.
        Aspose.Pdf.Forms.SignatureCustomAppearance customAppearance = new Aspose.Pdf.Forms.SignatureCustomAppearance();
        customAppearance.BackgroundColor = Aspose.Pdf.Color.FromRgb(0.9, 0.9, 0.9); // light gray background
        // Optionally adjust other appearance properties (foreground color, labels, etc.)
        customAppearance.ForegroundColor = Aspose.Pdf.Color.Black;
        pkcs1.CustomAppearance = customAppearance;

        // Define the rectangle where the signature will be placed (System.Drawing.Rectangle is required)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Use PdfFileSignature (facade) to apply the signature
        using (Aspose.Pdf.Facades.PdfFileSignature pdfSigner = new Aspose.Pdf.Facades.PdfFileSignature())
        {
            // Bind the source PDF
            pdfSigner.BindPdf(inputPdf);

            // Optional: set a graphic appearance (e.g., an image) for the signature field
            // pdfSigner.SignatureAppearance = "signature_image.png";

            // Sign the document on page 1 (pages are 1‑based)
            pdfSigner.Sign(1, true, rect, pkcs1);

            // Save the signed PDF
            pdfSigner.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}