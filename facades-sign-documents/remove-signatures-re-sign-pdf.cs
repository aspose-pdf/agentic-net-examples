using System;
using System.Drawing; // for Rectangle
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "output_signed.pdf";
        const string certFile   = "new_certificate.pfx";
        const string certPass   = "certPassword";

        // Ensure the source file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Initialize the facade
        PdfFileSignature pdfSign = new PdfFileSignature();

        // Bind the existing PDF
        pdfSign.BindPdf(inputPdf);

        // Remove any existing signatures (all signatures are cleared)
        pdfSign.RemoveSignatures();

        // Set the new certificate for signing
        pdfSign.SetCertificate(certFile, certPass);

        // Optional: set a visual appearance for the signature (image file)
        // pdfSign.SignatureAppearance = "signature_image.jpg";

        // Define the rectangle where the visible signature will be placed
        // Rectangle(x, y, width, height) – coordinates are in points (1/72 inch)
        Rectangle rect = new Rectangle(100, 100, 200, 50);

        // Sign the document on page 1 (pages are 1‑based)
        pdfSign.Sign(
            page: 1,                     // page number
            SigReason: "Updated signature",
            SigContact: "contact@example.com",
            SigLocation: "New York",
            visible: true,               // make the signature visible
            annotRect: rect);            // rectangle for the signature

        // Save the newly signed PDF
        pdfSign.Save(outputPdf);

        // Release resources
        pdfSign.Close();

        Console.WriteLine($"Signature removed and re‑signed successfully. Output: {outputPdf}");
    }
}