using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <pdfPath> <signatureImagePath>");
            return;
        }

        string pdfPath = args[0];
        string imagePath = args[1];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Signature image file not found: {imagePath}");
            return;
        }

        using (Document document = new Document(pdfPath))
        {
            using (PdfFileSignature pdfSignature = new PdfFileSignature())
            {
                pdfSignature.BindPdf(document);
                pdfSignature.SignatureAppearance = imagePath;

                // System.Drawing.Rectangle expects (x, y, width, height)
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(
                    100, // x (LLX)
                    100, // y (LLY)
                    100, // width (URX - LLX)
                    50   // height (URY - LLY)
                );

                // Sign on page 1
                pdfSignature.Sign(1, "Signed by Aspose.Pdf", "Contact", "Location", true, rect);
            }

            document.Save("signed.pdf");
        }

        Console.WriteLine("PDF signed and saved as 'signed.pdf'.");
    }
}
