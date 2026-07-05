using System;
using System.Drawing;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input PDF path and signature image path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <app> <input-pdf> <signature-image>");
            return;
        }

        string inputPdf = args[0];
        string signatureImage = args[1];
        string outputPdf = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(inputPdf) ?? "",
            System.IO.Path.GetFileNameWithoutExtension(inputPdf) + "_signed.pdf");

        // Verify input files exist
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!System.IO.File.Exists(signatureImage))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImage}");
            return;
        }

        // Create the PdfFileSignature facade
        PdfFileSignature pdfSign = new PdfFileSignature();

        // Bind the source PDF
        pdfSign.BindPdf(inputPdf);

        // Set the visual appearance of the signature (the image)
        pdfSign.SignatureAppearance = signatureImage;

        // Define the rectangle where the signature will be placed (in points)
        // Here we place it at (100,100) with width=200 and height=100
        Rectangle rect = new Rectangle(100, 100, 200, 100);

        // Sign the document.
        // Note: No certificate is set; this will add a visible signature appearance only.
        // If a digital certificate is required, call pdfSign.SetCertificate(pfxPath, password) before signing.
        pdfSign.Sign(
            page: 1,                 // first page (1‑based indexing)
            SigReason: "Signed",     // reason for signing
            SigContact: "contact@example.com",
            SigLocation: "Location",
            visible: true,           // make the signature visible
            annotRect: rect);        // rectangle for the signature

        // Save the signed PDF
        pdfSign.Save(outputPdf);

        Console.WriteLine($"Signed PDF saved to: {outputPdf}");
    }
}