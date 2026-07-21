using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileSignature and related facade classes

class Program
{
    static void Main()
    {
        // Paths for the input PDF, output PDF and the new certificate
        const string inputPdfPath   = "input.pdf";
        const string outputPdfPath  = "output_signed.pdf";
        const string certificatePath = "newcert.pfx";
        const string certificatePassword = "newpassword";

        // Ensure the input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Use the PdfFileSignature facade to manipulate signatures
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Load the PDF document
            pdfSignature.BindPdf(inputPdfPath);

            // Remove any existing signatures (if present)
            pdfSignature.RemoveSignatures();

            // Set the new certificate that will be used for signing
            pdfSignature.SetCertificate(certificatePath, certificatePassword);

            // Optional: set a visual appearance for the signature (e.g., an image)
            // pdfSignature.SignatureAppearance = "signature_appearance.png";

            // Define the rectangle where the visible signature will be placed
            // System.Drawing.Rectangle is required by the facade API
            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 50);

            // Sign the first page of the PDF
            // Parameters: page number (1‑based), reason, contact, location, visibility, rectangle
            pdfSignature.Sign(
                page: 1,
                SigReason: "Updated signature",
                SigContact: "contact@example.com",
                SigLocation: "New York",
                visible: true,
                annotRect: signatureRect);

            // Save the signed PDF to the output path
            pdfSignature.Save(outputPdfPath);
        }

        Console.WriteLine($"Signature removed and PDF re‑signed successfully: {outputPdfPath}");
    }
}