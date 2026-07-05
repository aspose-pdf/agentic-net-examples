using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "signed_output.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string appearanceImage = "signature.png"; // optional appearance image

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

        // Bind the PDF, set certificate and appearance, then sign
        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);

            // Optional: set a graphic appearance for the signature
            if (File.Exists(appearanceImage))
                pdfSign.SignatureAppearance = appearanceImage;

            // Set the signing certificate
            pdfSign.SetCertificate(certPath, certPassword);

            // Determine the last page (Aspose.Pdf uses 1‑based indexing)
            int lastPage = pdfSign.Document.Pages.Count;

            // Retrieve page dimensions
            Page page = pdfSign.Document.Pages[lastPage];
            double pageWidth = page.PageInfo.Width;
            double pageHeight = page.PageInfo.Height;

            // Define signature rectangle size and position (bottom‑right corner)
            int sigWidth = 150;   // width in points
            int sigHeight = 50;   // height in points
            int margin = 20;      // margin from page edges

            int x = (int)(pageWidth - sigWidth - margin); // left coordinate
            int y = margin;                               // bottom coordinate

            var rect = new System.Drawing.Rectangle(x, y, sigWidth, sigHeight);

            // Apply the visible signature
            pdfSign.Sign(
                lastPage,               // page number
                "Document approved",    // reason
                "contact@example.com",  // contact
                "Office",               // location
                true,                   // visible
                rect);                  // signature rectangle

            // Save the signed PDF
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}