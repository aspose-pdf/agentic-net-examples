using System;
using System.Drawing;
using System.IO;
using Aspose.Pdf.Facades;

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
            Console.Error.WriteLine($"Error: PDF file not found: {pdfPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Error: Signature image not found: {imagePath}");
            return;
        }

        // Output file will be placed alongside the input file with "_signed.pdf" suffix
        string outputPath = Path.Combine(
            Path.GetDirectoryName(pdfPath) ?? string.Empty,
            Path.GetFileNameWithoutExtension(pdfPath) + "_signed.pdf");

        try
        {
            // Initialize the PdfFileSignature facade
            using (PdfFileSignature pdfSign = new PdfFileSignature())
            {
                // Bind the source PDF
                pdfSign.BindPdf(pdfPath);

                // Set the visual appearance of the signature (image)
                pdfSign.SignatureAppearance = imagePath;

                // Define signature properties
                int pageNumber = 1; // first page (1‑based indexing)
                string reason = "Document signed";
                string contact = "contact@example.com";
                string location = "Location";
                bool visible = true;

                // Define the rectangle where the signature will appear
                // Rectangle(x, y, width, height) – coordinates are in points (1/72 inch)
                Rectangle rect = new Rectangle(100, 100, 200, 100);

                // Apply the signature
                pdfSign.Sign(pageNumber, reason, contact, location, visible, rect);

                // Save the signed PDF
                pdfSign.Save(outputPath);
            }

            Console.WriteLine($"Signed PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during signing: {ex.Message}");
        }
    }
}