using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_hidden.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(certPath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certPath}");
            return;
        }

        // Create the PdfFileSignature facade and bind the source PDF
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Set the certificate used for signing
            pdfSign.SetCertificate(certPath, certPassword);

            // Define a rectangle (required even when the signature is invisible)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 200);

            // Sign the document with visibility set to false (no appearance)
            pdfSign.Sign(
                page: 1,                 // 1‑based page index
                SigReason: "Document approved",
                SigContact: "john.doe@example.com",
                SigLocation: "New York",
                visible: false,          // hide the signature appearance
                annotRect: rect);        // rectangle placeholder

            // Save the signed PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}' with hidden appearance.");
    }
}