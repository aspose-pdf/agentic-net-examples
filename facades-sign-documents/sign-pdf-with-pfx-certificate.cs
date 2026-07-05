using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "password";

        // Verify that the source PDF and the certificate file exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {pfxPath}");
            return;
        }

        try
        {
            // Create the PdfFileSignature facade
            PdfFileSignature pdfSign = new PdfFileSignature();

            // Bind the PDF that will be signed
            pdfSign.BindPdf(inputPath);

            // Optional: set an image that will be shown as the signature appearance
            // pdfSign.SignatureAppearance = "signature.png";

            // Provide the PFX certificate and its password
            pdfSign.SetCertificate(pfxPath, pfxPassword);

            // Define the rectangle (x, y, width, height) where the visible signature will appear
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

            // Sign the document on page 1 (pages are 1‑based)
            pdfSign.Sign(
                page: 1,
                SigReason: "Approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York",
                visible: true,
                annotRect: rect);

            // Save the signed PDF to the output file
            pdfSign.Save(outputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error signing PDF: {ex.Message}");
        }
    }
}