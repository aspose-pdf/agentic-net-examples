using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and Base64-encoded PFX certificate
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string base64Pfx     = "<BASE64_PFX_STRING>"; // replace with actual Base64 string
        const string certPassword  = ""; // if the PFX has a password, set it here

        // Validate input file existence
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Decode the Base64 PFX and create an X509Certificate2 instance
        byte[] pfxBytes;
        try
        {
            pfxBytes = Convert.FromBase64String(base64Pfx);
        }
        catch (FormatException ex)
        {
            Console.Error.WriteLine($"Invalid Base64 certificate data: {ex.Message}");
            return;
        }

        X509Certificate2 certificate;
        try
        {
            certificate = string.IsNullOrEmpty(certPassword)
                ? new X509Certificate2(pfxBytes)
                : new X509Certificate2(pfxBytes, certPassword, X509KeyStorageFlags.MachineKeySet);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load certificate: {ex.Message}");
            return;
        }

        // Create an ExternalSignature object that wraps the certificate
        ExternalSignature externalSignature = new ExternalSignature(certificate);
        externalSignature.Reason      = "Document approved";
        externalSignature.ContactInfo = "contact@example.com";
        externalSignature.Location    = "New York";

        // Define the visible signature rectangle (System.Drawing.Rectangle)
        // Parameters: x, y, width, height
        System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // Sign the PDF using PdfFileSignature
        using (PdfFileSignature pdfSigner = new PdfFileSignature())
        {
            // Bind the source PDF
            pdfSigner.BindPdf(inputPdfPath);

            // Optional: set a visual appearance image for the signature
            // pdfSigner.SignatureAppearance = "signature_image.jpg";

            // Sign on page 1 (pages are 1‑based)
            pdfSigner.Sign(
                page: 1,
                SigReason: externalSignature.Reason,
                SigContact: externalSignature.ContactInfo,
                SigLocation: externalSignature.Location,
                visible: true,
                annotRect: signatureRect,
                sig: externalSignature);

            // Save the signed PDF
            pdfSigner.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed successfully and saved to '{outputPdfPath}'.");
    }
}