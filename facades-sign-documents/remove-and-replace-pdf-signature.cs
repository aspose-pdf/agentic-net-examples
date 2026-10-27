using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "signed_input.pdf";
        const string outputPdf = "resigned_output.pdf";
        const string certPath = "new_certificate.pfx";
        const string certPassword = "certPassword";

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

        try
        {
            // Bind the PDF to the signature facade
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                pdfSignature.BindPdf(inputPdf);

                // Remove all existing signatures (if any)
                var existingSignatures = pdfSignature.GetSignatureNames(true);
                foreach (var sigName in existingSignatures)
                {
                    pdfSignature.RemoveSignature(sigName);
                }

                // Create PKCS7 signature object with the new certificate
                PKCS7 pkcs7 = new PKCS7(certPath, certPassword)
                {
                    Reason = "Document updated",
                    ContactInfo = "contact@example.com",
                    Location = "New York"
                };

                // Optional: customise visual appearance of the signature
                SignatureCustomAppearance customAppearance = new SignatureCustomAppearance
                {
                    ShowContactInfo = true,
                    ShowLocation = true
                };
                pkcs7.CustomAppearance = customAppearance;

                // Sign the PDF on the first page at the desired rectangle.
                // System.Drawing.Rectangle uses (x, y, width, height).
                // The original Aspose rectangle (100,100,250,150) translates to width=150, height=50.
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 150, 50);
                pdfSignature.Sign(1, true, rect, pkcs7);

                // Save the updated PDF
                pdfSignature.Save(outputPdf);
            }

            Console.WriteLine($"PDF re-signed successfully: {outputPdf}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
