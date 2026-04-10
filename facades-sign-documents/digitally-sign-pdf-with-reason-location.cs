using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf      = "input.pdf";
        const string outputPdf     = "signed.pdf";
        const string certificate   = "certificate.pfx";
        const string certPassword  = "password";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the signature facade and bind the source PDF
        PdfFileSignature signer = new PdfFileSignature();
        signer.BindPdf(inputPdf);

        // Optional: set a visual appearance for the signature (image file path)
        // signer.SignatureAppearance = "signature_appearance.png";

        // Provide the signing certificate
        signer.SetCertificate(certificate, certPassword);

        // Define the rectangle where the signature will appear (x, y, width, height)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 50);

        // Sign page 1 with the required reason and location
        signer.Sign(
            page: 1,
            SigReason: "Approved for release",
            SigContact: "",               // No contact information
            SigLocation: "New York",
            visible: true,
            annotRect: rect);

        // Save the signed document
        signer.Save(outputPdf);
        signer.Close();

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}