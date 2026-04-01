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
        const string appearancePath = "signature.jpg";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(certificatePath))
        {
            Console.Error.WriteLine($"Certificate file not found: {certificatePath}");
            return;
        }

        // Determine rectangle for the visible signature on the bottom‑right corner of the last page
        int lastPageNumber;
        int signatureWidth = 150;
        int signatureHeight = 50;
        int margin = 20;
        System.Drawing.Rectangle signatureRect;

        using (Document doc = new Document(inputPath))
        {
            lastPageNumber = doc.Pages.Count;
            Page lastPage = doc.Pages[lastPageNumber];
            double pageWidth = lastPage.PageInfo.Width;
            double pageHeight = lastPage.PageInfo.Height;

            int x = (int)(pageWidth - signatureWidth - margin);
            int y = margin; // distance from bottom edge
            signatureRect = new System.Drawing.Rectangle(x, y, signatureWidth, signatureHeight);
        }

        using (PdfFileSignature pdfSign = new PdfFileSignature())
        {
            pdfSign.BindPdf(inputPath);
            pdfSign.SetCertificate(certificatePath, certificatePassword);
            pdfSign.SignatureAppearance = appearancePath;
            pdfSign.Sign(lastPageNumber, "Document signed", "John Doe", "Bottom‑Right", true, signatureRect);
            pdfSign.Save(outputPath);
        }

        Console.WriteLine($"Signed PDF saved as '{outputPath}'.");
    }
}
