using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "signed.pdf";
        const string certificatePath = "newcert.pfx";
        const string certificatePassword = "password";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Bind the PDF, remove existing signatures, and apply a new signature
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPdfPath);

            // Remove all existing signatures
            pdfSignature.RemoveSignatures();

            // Prepare the certificate for signing
            PKCS1 pkcs1 = new PKCS1(certificatePath, certificatePassword);

            // Define the visible signature rectangle (x, y, width, height)
            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(100, 100, 200, 50);

            // Apply the new signature on page 1
            pdfSignature.Sign(
                1,                                 // page number (1‑based)
                "Updated signature",              // reason
                "contact@example.com",            // contact info
                "New York, USA",                  // location
                true,                              // visible signature
                signatureRect,                     // rectangle for appearance
                pkcs1);                            // certificate object

            // Save the signed PDF
            pdfSignature.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF signed and saved to '{outputPdfPath}'.");
    }
}