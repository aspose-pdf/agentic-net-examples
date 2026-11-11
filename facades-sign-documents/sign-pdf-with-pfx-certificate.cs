using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // Added namespace for PKCS7

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string pfxPath = "certificate.pfx";
        const string pfxPassword = "yourPfxPassword";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(pfxPath))
        {
            Console.Error.WriteLine($"PFX file not found: {pfxPath}");
            return;
        }

        try
        {
            // Create a PKCS7 signature object directly from the PFX file.
            PKCS7 pkcs = new PKCS7(pfxPath, pfxPassword)
            {
                Reason = "Document signed",
                ContactInfo = "contact@example.com",
                Location = "My Company"
            };

            // Define the visible signature rectangle (coordinates are in points).
            Rectangle signatureRect = new Rectangle(100, 100, 200, 50);

            using (PdfFileSignature signer = new PdfFileSignature())
            {
                signer.BindPdf(inputPdf);

                // Sign page 1, make the signature visible, and use the PKCS7 object.
                signer.Sign(1, true, signatureRect, pkcs);

                // Save the signed document.
                signer.Save(outputPdf);
            }

            Console.WriteLine($"PDF signed successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error signing PDF: {ex.Message}");
        }
    }
}
