using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "certPassword";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Determine the rectangle for the visible signature on page 2 (bottom‑right corner)
            const float margin = 10f;          // margin from page edges
            const float sigWidth = 150f;       // width of signature appearance
            const float sigHeight = 50f;       // height of signature appearance

            // Aspose.Pdf uses 1‑based page indexing
            Page targetPage = pdfDoc.Pages[2];
            // PageInfo.Width/Height return double – cast to float for rectangle calculations
            float pageWidth = (float)targetPage.PageInfo.Width;
            float pageHeight = (float)targetPage.PageInfo.Height;

            // Lower‑left (llx,lly) and upper‑right (urx,ury) coordinates
            float llx = pageWidth - sigWidth - margin;
            float lly = margin;
            float urx = pageWidth - margin;
            float ury = sigHeight + margin;

            // Create the signature appearance rectangle
            Aspose.Pdf.Rectangle sigRect = new Aspose.Pdf.Rectangle(llx, lly, urx, ury);

            // Create a visible signature field on page 2 and add it to the form
            SignatureField signatureField = new SignatureField(targetPage, sigRect);
            signatureField.PartialName = "Signature1";
            pdfDoc.Form.Add(signatureField, targetPage.Number);

            // Prepare PKCS7 credentials (the concrete Signature implementation)
            PKCS7 pkcs = new PKCS7(certPath, certPassword);
            pkcs.Reason = "Document approved";
            pkcs.Location = "Head Office";
            pkcs.ContactInfo = "contact@example.com";

            // Configure a simple visual appearance (background color and font)
            pkcs.CustomAppearance = new SignatureCustomAppearance
            {
                BackgroundColor = Aspose.Pdf.Color.FromArgb(80, 240, 240, 255), // semi‑transparent
                FontFamilyName = "Arial",
                FontSize = 9,
                ShowLocation = true,
                ShowReason = true
            };

            // Sign the field (the field's Signature property becomes read‑only after this call)
            signatureField.Sign(pkcs);

            // Save the signed PDF
            pdfDoc.Save(outputPdfPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdfPath}'.");
    }
}
