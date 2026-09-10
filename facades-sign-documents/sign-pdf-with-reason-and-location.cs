using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "signed_output.pdf";
        const string certPath  = "certificate.pfx";
        const string certPass  = "password";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Define the rectangle where the visible signature will appear
        // (x, y, width, height) – coordinates are in points (1/72 inch)
        Rectangle signatureRect = new Rectangle(100, 100, 200, 100);

        // Use PdfFileSignature facade to sign the PDF
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            // Bind the existing PDF document
            pdfSign.BindPdf(inputPdf);

            // Set the certificate used for signing
            pdfSign.SetCertificate(certPath, certPass);

            // Sign the document:
            // page number (1‑based), reason, contact (empty), location,
            // visibility (true for visible signature), and rectangle.
            pdfSign.Sign(
                page: 1,
                SigReason: "Approved for release",
                SigContact: "",
                SigLocation: "New York",
                visible: true,
                annotRect: signatureRect);

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}