using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPdf   = "input.pdf";
        const string outputPdf  = "signed_output.pdf";
        const string certPath   = "certificate.pfx";
        const string certPass   = "password";
        const string appearance = "signature_appearance.jpg"; // optional image for visible signature

        // Verify that the source PDF and certificate exist
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

        // Create the PdfFileSignature facade and bind the source PDF
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPdf);

            // Determine the last page number (Aspose.Pdf uses 1‑based indexing)
            int lastPageNumber = pdfSign.Document.Pages.Count;

            // Retrieve page dimensions
            double pageWidth  = pdfSign.Document.Pages[lastPageNumber].PageInfo.Width;
            double pageHeight = pdfSign.Document.Pages[lastPageNumber].PageInfo.Height;

            // Desired size of the visible signature (in points)
            const int sigWidth  = 150; // width
            const int sigHeight = 50;  // height

            // Calculate rectangle positioned at the bottom‑right corner
            // System.Drawing.Rectangle is used for the signature API; fully qualified to avoid ambiguity
            System.Drawing.Rectangle sigRect = new System.Drawing.Rectangle(
                (int)(pageWidth  - sigWidth), // lower‑left X
                (int)(pageHeight - sigHeight), // lower‑left Y
                sigWidth,
                sigHeight);

            // Optional: set a graphic appearance for the signature
            if (File.Exists(appearance))
                pdfSign.SignatureAppearance = appearance;

            // Provide the signing certificate
            pdfSign.SetCertificate(certPath, certPass);

            // Apply the visible signature on the last page
            pdfSign.Sign(
                lastPageNumber,          // page number
                "Document approved",     // reason
                "contact@example.com",   // contact
                "Head Office",           // location
                true,                    // visible
                sigRect);                // rectangle

            // Save the signed PDF
            pdfSign.Save(outputPdf);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}