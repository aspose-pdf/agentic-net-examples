using System;
using System.IO;
using Aspose.Pdf.Facades;
using System.Drawing;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <pdfPath> <signatureImagePath>");
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
            Console.Error.WriteLine($"Signature image not found: {imagePath}");
            return;
        }

        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            // Bind the source PDF
            pdfSignature.BindPdf(pdfPath);

            // Set the visual appearance of the signature (image file)
            pdfSignature.SignatureAppearance = imagePath;

            // Define the rectangle where the signature will be placed (x, y, width, height)
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 50);

            // Sign the first page with visible signature
            pdfSignature.Sign(1, "Signed by Aspose", "contact@example.com", "Location", true, rect);

            // Save the signed PDF (output file name must be simple)
            pdfSignature.Save("signed.pdf");
        }

        Console.WriteLine("PDF signed and saved as signed.pdf");
    }
}