using System;
using System.IO;
using System.Drawing;                     // for Rectangle
using Aspose.Pdf.Facades;                // PdfFileSignature, PKCS1
using Aspose.Pdf.Forms;                  // Signature base class

class Program
{
    static void Main()
    {
        // Input PDF, output PDF, and certificate (PFX) details
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";

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
            // Create the signature object with certificate and password
            PKCS1 pkcs1 = new PKCS1(certPath, certPass);
            pkcs1.Reason   = "Approved for release"; // set signature reason
            pkcs1.Location = "New York";             // set signature location
            // Optional: set contact information if desired
            // pkcs1.ContactInfo = "contact@example.com";

            // Define the visible rectangle for the signature (x, y, width, height)
            Rectangle signatureRect = new Rectangle(100, 100, 200, 50);

            // Initialize the PdfFileSignature facade and bind the source PDF
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                pdfSignature.BindPdf(inputPdf);

                // Optional: set a graphic appearance for the signature (e.g., an image)
                // pdfSignature.SignatureAppearance = "signature_image.jpg";

                // Sign the document: page 1, visible, rectangle, and the prepared PKCS1 object
                pdfSignature.Sign(page: 1, visible: true, annotRect: signatureRect, sig: pkcs1);

                // Save the signed PDF
                pdfSignature.Save(outputPdf);
            }

            Console.WriteLine($"PDF signed successfully. Output saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}