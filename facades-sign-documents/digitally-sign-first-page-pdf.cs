using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

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

        try
        {
            // Create the PdfFileSignature facade
            PdfFileSignature signer = new PdfFileSignature();

            // Bind the PDF document to the facade
            signer.BindPdf(inputPdf);

            // Optional: set a visual appearance for the signature (image file path)
            // signer.SignatureAppearance = "signature_image.jpg";

            // Provide the certificate and its password
            signer.SetCertificate(certPath, certPassword);

            // Define the rectangle where the signature will be placed on page 1
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 200);

            // Sign page 1 with reason, contact, location, visibility and rectangle
            signer.Sign(
                page: 1,
                SigReason: "Document approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York",
                visible: true,
                annotRect: rect);

            // Save the signed PDF
            signer.Save(outputPdf);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}