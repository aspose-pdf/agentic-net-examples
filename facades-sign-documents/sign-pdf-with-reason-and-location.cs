using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF
        const string outputPdf = "signed_output.pdf";  // signed PDF
        const string certPath  = "certificate.pfx";   // path to your PFX certificate
        const string certPass  = "password";          // certificate password

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
            // Initialize the facade and bind the PDF document
            using (PdfFileSignature pdfSign = new PdfFileSignature())
            {
                pdfSign.BindPdf(inputPdf);

                // Create a PKCS#1 signature object and set required properties
                PKCS1 signature = new PKCS1(certPath, certPass)
                {
                    Reason   = "Approved for release",
                    Location = "New York"
                    // ContactInfo can be set here if needed, e.g. ContactInfo = "john.doe@example.com"
                };

                // Optional: set a visual appearance for the signature (image file)
                // pdfSign.SignatureAppearance = "signature_appearance.png";

                // Define the rectangle where the signature will be placed (x, y, width, height)
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

                // Sign the document on page 1, make the signature visible
                pdfSign.Sign(1, true, rect, signature);

                // Save the signed PDF
                pdfSign.Save(outputPdf);
            }

            Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}