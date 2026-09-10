using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing; // needed for Rectangle

class Program
{
    static void Main()
    {
        // Input PDF, output signed PDF, certificate file and its password
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certificatePfx = "certificate.pfx";
        const string certificatePassword = "password";

        // Verify that required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(certificatePfx))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePfx}");
            return;
        }

        try
        {
            // Initialize the PdfFileSignature facade
            using (PdfFileSignature signer = new PdfFileSignature())
            {
                // Bind the PDF file to be signed
                signer.BindPdf(inputPdf);

                // Optional: set a visual appearance for the signature (image file)
                // signer.SignatureAppearance = "signature_image.jpg";

                // Provide the certificate (PFX) and its password
                signer.SetCertificate(certificatePfx, certificatePassword);

                // Define the rectangle where the visible signature will be placed
                // Rectangle(x, y, width, height) – coordinates are in points (1/72 inch)
                Rectangle rect = new Rectangle(100, 100, 200, 100);

                // Sign the document on page 1 with desired metadata
                signer.Sign(
                    page: 1,
                    SigReason: "Document approved",
                    SigContact: "john.doe@example.com",
                    SigLocation: "New York",
                    visible: true,
                    annotRect: rect
                );

                // Save the signed PDF
                signer.Save(outputPdf);
            }

            Console.WriteLine($"PDF signed successfully and saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}