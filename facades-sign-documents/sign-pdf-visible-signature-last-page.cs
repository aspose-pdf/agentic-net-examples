using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "signed.pdf";
        const string certPath = "certificate.pfx";
        const string certPassword = "password";
        const string appearanceImage = "signature.png"; // optional appearance image

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

        // Load the PDF to obtain page dimensions and count
        using (Document doc = new Document(inputPdf))
        {
            int lastPageNumber = doc.Pages.Count; // 1‑based indexing
            double pageWidth = doc.Pages[lastPageNumber].PageInfo.Width;
            double pageHeight = doc.Pages[lastPageNumber].PageInfo.Height;

            // Define a rectangle for the visible signature (e.g., 150×50 points)
            // placed 20 points from the right and bottom edges
            int sigWidth = 150;
            int sigHeight = 50;
            int margin = 20;
            int llx = (int)(pageWidth - sigWidth - margin);
            int lly = (int)(pageHeight - sigHeight - margin);
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(llx, lly, sigWidth, sigHeight);

            // Create the signature facade, bind the PDF, set certificate and appearance, then sign
            using (PdfFileSignature pdfSign = new PdfFileSignature())
            {
                pdfSign.BindPdf(inputPdf);
                pdfSign.SignatureAppearance = appearanceImage; // optional visual appearance
                pdfSign.SetCertificate(certPath, certPassword);

                // Sign the last page with a visible signature
                pdfSign.Sign(lastPageNumber, "Document signed", "contact@example.com", "Location", true, rect);
                pdfSign.Save(outputPdf);
            }
        }

        Console.WriteLine($"Signed PDF saved to '{outputPdf}'.");
    }
}