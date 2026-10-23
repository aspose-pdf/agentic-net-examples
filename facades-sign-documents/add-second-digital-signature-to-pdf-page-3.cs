using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the source PDF, output PDF and the signing certificate (PFX)
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed_output.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "certPassword";

        // Verify that required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Load the PDF document
        Document doc = new Document(inputPdfPath);

        // First digital signature on page 3, rectangle (100,500,200,550)
        // Aspose.Pdf.Rectangle uses (lowerLeftX, lowerLeftY, upperRightX, upperRightY)
        SignatureField sigField1 = new SignatureField(doc, new Aspose.Pdf.Rectangle(100, 500, 200, 550))
        {
            Name = "Signature1",
            // Optional: set reason, location, contact info via custom properties if needed
        };
        doc.Form.Add(sigField1);

        // Second digital signature on page 3, rectangle (300,500,400,550)
        SignatureField sigField2 = new SignatureField(doc, new Aspose.Pdf.Rectangle(300, 500, 400, 550))
        {
            Name = "Signature2",
        };
        doc.Form.Add(sigField2);

        // Create PKCS7 object from the certificate (used for both signatures)
        PKCS7 pkcs7 = new PKCS7(certificatePath, certificatePassword);

        // Sign each field
        sigField1.Sign(pkcs7);
        sigField2.Sign(pkcs7);

        // Save the signed PDF to the output file
        doc.Save(outputPdfPath);

        Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdfPath}'.");
    }
}
