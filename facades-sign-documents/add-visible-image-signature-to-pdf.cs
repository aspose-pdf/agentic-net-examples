using System;
using System.IO;
using System.Drawing;               // Required for System.Drawing.Rectangle
using Aspose.Pdf.Facades;          // Facade API for signing

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

        // Prepare output file name (e.g., original_signed.pdf)
        string outputPath = Path.Combine(
            Path.GetDirectoryName(pdfPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(pdfPath) + "_signed.pdf");

        try
        {
            // PdfFileSignature implements IDisposable via SaveableFacade, so use using
            using (PdfFileSignature signer = new PdfFileSignature())
            {
                // Bind the source PDF
                signer.BindPdf(pdfPath);

                // Set the visual appearance of the signature (image file)
                signer.SignatureAppearance = imagePath;

                // Define the rectangle where the signature will appear (page coordinates)
                // Rectangle(left, top, width, height) – System.Drawing.Rectangle uses (x, y, width, height)
                // Here we place it at (100, 100) with size 200x100 points.
                Rectangle rect = new Rectangle(100, 100, 200, 100);

                // Sign the document.
                // Parameters: page number (1‑based), reason, contact, location, visibility, rectangle.
                // No digital certificate is set; this creates a visible signature appearance only.
                signer.Sign(
                    page: 1,
                    SigReason: "Signed",
                    SigContact: string.Empty,
                    SigLocation: string.Empty,
                    visible: true,
                    annotRect: rect);

                // Save the signed PDF
                signer.Save(outputPath);
            }

            Console.WriteLine($"Signed PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}