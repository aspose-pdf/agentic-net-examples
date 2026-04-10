using System;
using System.IO;
using System.Drawing;               // For Rectangle used by PdfFileSignature
using Aspose.Pdf.Facades;          // Facade classes for PDF operations

class Program
{
    static void Main(string[] args)
    {
        // Expect at least two arguments: input PDF path and signature image path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <app> <pdfPath> <signatureImagePath>");
            return;
        }

        string pdfPath = args[0];
        string signatureImagePath = args[1];

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(signatureImagePath))
        {
            Console.Error.WriteLine($"Signature image not found: {signatureImagePath}");
            return;
        }

        // Output file will be placed alongside the source PDF with a suffix
        string outputPath = System.IO.Path.Combine(
            System.IO.Path.GetDirectoryName(pdfPath) ?? string.Empty,
            System.IO.Path.GetFileNameWithoutExtension(pdfPath) + "_signed.pdf");

        try
        {
            // Initialize the facade inside a using block to guarantee disposal
            using (PdfFileSignature pdfSign = new PdfFileSignature())
            {
                // Bind the source PDF
                pdfSign.BindPdf(pdfPath);

                // Set the visual appearance of the signature (image file)
                pdfSign.SignatureAppearance = signatureImagePath;

                // Define the rectangle where the signature will appear.
                // PdfFileSignature.Sign expects a System.Drawing.Rectangle.
                // (x, y, width, height) – place at (100,100) with width 200 and height 50.
                Rectangle rect = new Rectangle(100, 100, 200, 50);

                // Apply a visible signature on page 1.
                // This overload does not require a digital certificate.
                pdfSign.Sign(
                    page: 1,
                    SigReason: "Document approved",
                    SigContact: "contact@example.com",
                    SigLocation: "Office",
                    visible: true,
                    annotRect: rect);

                // Save the signed PDF
                pdfSign.Save(outputPath);
            }

            Console.WriteLine($"Signed PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
