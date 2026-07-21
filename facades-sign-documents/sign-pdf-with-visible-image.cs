using System;
using System.IO;
using System.Drawing; // needed for System.Drawing.Rectangle
using Aspose.Pdf.Facades;

class Program
{
    static void Main(string[] args)
    {
        // Expect exactly two arguments: PDF file path and signature image path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <app> <pdfPath> <signatureImagePath>");
            return;
        }

        string pdfPath = args[0];
        string imagePath = args[1];

        // Validate input files
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

        // Prepare output file name
        string outputPath = Path.Combine(
            Path.GetDirectoryName(pdfPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(pdfPath) + "_signed.pdf");

        // Initialize the PdfFileSignature facade
        PdfFileSignature pdfSign = new PdfFileSignature();

        // Bind the source PDF
        pdfSign.BindPdf(pdfPath);

        // Set the visual appearance of the signature (image)
        pdfSign.SignatureAppearance = imagePath;

        // Define the rectangle where the signature will be placed (page coordinates)
        // Rectangle(x, y, width, height) – coordinates are in points (1/72 inch)
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle(100, 100, 200, 100);

        // OPTIONAL: If you have a certificate, set it here.
        // The example uses placeholder values; replace with actual certificate path and password.
        try
        {
            pdfSign.SetCertificate("certificate.pfx", "password");
            // Sign the first page with visible signature
            pdfSign.Sign(
                page: 1,
                SigReason: "Document signed",
                SigContact: "contact@example.com",
                SigLocation: "Location",
                visible: true,
                annotRect: rect);
        }
        catch (Exception ex)
        {
            // If certificate is missing or signing fails, report but still save the PDF with appearance
            Console.Error.WriteLine($"Signing failed (certificate may be missing): {ex.Message}");
        }

        // Save the signed PDF
        pdfSign.Save(outputPath);

        Console.WriteLine($"Signed PDF saved to '{outputPath}'.");
    }
}