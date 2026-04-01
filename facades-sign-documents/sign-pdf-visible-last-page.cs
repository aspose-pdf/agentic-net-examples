using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed.pdf";
        const string certificatePath = "certificate.pfx";
        const string certificatePassword = "password";
        const string appearancePath = "signature.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }
        if (!File.Exists(appearancePath))
        {
            Console.Error.WriteLine($"Signature appearance image not found: {appearancePath}");
            return;
        }

        // Load the document to obtain the size of the last page
        using (Document document = new Document(inputPath))
        {
            Page lastPage = document.Pages[document.Pages.Count];
            double pageWidth = lastPage.PageInfo.Width;
            double pageHeight = lastPage.PageInfo.Height;

            // Define signature rectangle (bottom‑right corner)
            int signatureWidth = 150;   // points
            int signatureHeight = 50;   // points
            int margin = 20;            // points from edges
            int rectX = (int)(pageWidth - signatureWidth - margin);
            int rectY = margin; // distance from bottom edge (PDF coordinate system)

            // PdfFileSignature expects a System.Drawing.Rectangle
            System.Drawing.Rectangle signatureRect = new System.Drawing.Rectangle(
                rectX,
                rectY,
                signatureWidth,
                signatureHeight);

            // Create and configure the signature facade
            PdfFileSignature pdfSignature = new PdfFileSignature();
            pdfSignature.BindPdf(inputPath);
            pdfSignature.SetCertificate(certificatePath, certificatePassword);
            pdfSignature.SignatureAppearance = appearancePath;

            // Sign the last page with a visible signature
            pdfSignature.Sign(
                document.Pages.Count,
                "Document signed",
                "contact@example.com",
                "Location",
                true,
                signatureRect);

            // Save the signed PDF
            pdfSignature.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}
