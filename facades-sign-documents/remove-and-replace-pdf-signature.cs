using System;
using System.IO;
using System.Drawing; // for Rectangle
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF that may already contain a signature
        const string inputPdf = "input.pdf";
        // Output PDF after removing old signature(s) and applying a new one
        const string outputPdf = "signed_updated.pdf";
        // Path to the new certificate (PFX) and its password
        const string certPath = "new_certificate.pfx";
        const string certPassword = "newPassword";

        // Optional: image to be used as visible signature appearance
        const string signatureImage = "signature.png";

        // Verify input files exist
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

        // Use PdfFileSignature facade to manipulate signatures
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the existing PDF document
            pdfSign.BindPdf(inputPdf);

            // Remove all existing signatures (if any)
            pdfSign.RemoveSignatures();

            // Set the new certificate for signing
            pdfSign.SetCertificate(certPath, certPassword);

            // Optional: set a visual appearance for the signature
            if (File.Exists(signatureImage))
                pdfSign.SignatureAppearance = signatureImage;

            // Define signature properties
            int pageNumber = 1;                     // 1‑based page index
            string reason = "Document updated";
            string contact = "admin@example.com";
            string location = "Head Office";
            bool visible = true;                    // make signature visible
            // Rectangle defines the position and size of the signature on the page
            Rectangle rect = new Rectangle(100, 100, 200, 100); // x, y, width, height

            // Apply the new signature
            pdfSign.Sign(pageNumber, reason, contact, location, visible, rect);

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signature removed and new signature applied. Output saved to '{outputPdf}'.");
    }
}